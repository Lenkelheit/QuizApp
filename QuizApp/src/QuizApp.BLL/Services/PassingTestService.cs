using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AutoMapper;
using Newtonsoft.Json;

using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.Dto.PassingTest;
using QuizApp.Entities.Enums;
using QuizApp.BLL.Dto.TestEvent.Payloads;
using QuizApp.BLL.Settings;

namespace QuizApp.BLL.Services
{
    public class PassingTestService : IPassingTestService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ITestRepository testRepository;

        private readonly ITestEventRepository testEventRepository;

        private readonly ITestResultRepository testResultRepository;

        private readonly IMapper mapper;

        private readonly TimeErrorSetting timeErrorSetting;


        public PassingTestService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<TimeErrorSetting> timeErrorSetting)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.testRepository = unitOfWork.GetRepository<Test, ITestRepository>() ?? throw new NullReferenceException(nameof(testRepository));
            this.testEventRepository = unitOfWork.GetRepository<TestEvent, ITestEventRepository>() ?? throw new NullReferenceException(nameof(testEventRepository));
            this.testResultRepository = unitOfWork.GetRepository<TestResult, ITestResultRepository>() ?? throw new NullReferenceException(nameof(testResultRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.timeErrorSetting = timeErrorSetting?.Value ?? throw new ArgumentNullException(nameof(timeErrorSetting));
        }


        public async Task<CreatedTestResultDto> CreateTestResult(UserUrlDto userUrlDto)
        {
            var testEvents = testEventRepository.Get(e => e.SessionId == userUrlDto.SessionId);

            var eventsQuestionAnswered = testEvents.Where(e => e.EventType == EventType.QuestionAnswered).ToList();
            var payloadQuestions = eventsQuestionAnswered.Select(eventQuestion => JsonConvert.DeserializeObject<PayloadQuestion>(eventQuestion.Payload)).ToList();

            var eventTestStarted = testEvents.First(e => e.EventType == EventType.TestStarted);
            var payloadTest = JsonConvert.DeserializeObject<PayloadTest>(eventTestStarted.Payload);
            var test = testRepository.GetById(id: payloadTest.TestId, includeProperties: prop => prop.Include(t => t.TestQuestions).ThenInclude(q => q.TestQuestionOptions));

            var testResult = new TestResult
            {
                IntervieweeName = payloadTest.IntervieweeName,
                PassingStartTime = eventTestStarted.StartTime,
                PassingEndTime = DateTime.Now,
                UrlId = userUrlDto.UrlId
            };

            foreach (var question in test.TestQuestions)
            {
                var payloadQuestionIndex = payloadQuestions.FindIndex(q => q.QuestionId == question.Id);

                var resultAnswer = CreateResultAnswerWithOptions(payloadQuestionIndex >= 0 ? payloadQuestions[payloadQuestionIndex] : null,
                                                                          payloadQuestionIndex,
                                                                          eventsQuestionAnswered,
                                                                          eventTestStarted);
                resultAnswer.QuestionId = question.Id;

                testResult.ResultAnswers.Add(resultAnswer);

                testResult.Score += IsResultAnswerInTime(question, resultAnswer)
                                    ? CalculateQuestionScore(question, payloadQuestionIndex >= 0 ? payloadQuestions[payloadQuestionIndex] : null)
                                    : 0;
            }

            testResult.Score = IsAtLeastOneQuestionInTest(test) && IsTestResultInTime(test, testResult)
                               ? GetPercentageTestResultScore(testResult)
                               : 0;

            testEventRepository.Delete(testEvents);
            testResultRepository.Insert(testResult);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestResultDto>(testResult);
        }

        private ResultAnswer CreateResultAnswerWithOptions(PayloadQuestion receivedQuestion, int receivedQuestionIndex, List<TestEvent> eventsQuestionAnswered, TestEvent eventTestStarted)
        {
            var resultAnswer = new ResultAnswer();

            if (receivedQuestionIndex >= 0)
            {
                resultAnswer.TimeTakenSeconds = receivedQuestionIndex == 0
                    ? eventsQuestionAnswered[receivedQuestionIndex].StartTime - eventTestStarted.StartTime
                    : eventsQuestionAnswered[receivedQuestionIndex].StartTime - eventsQuestionAnswered[receivedQuestionIndex - 1].StartTime;

                resultAnswer.ResultAnswerOptions = receivedQuestion.SelectedOptionsId.Select(selectedOptionId => new ResultAnswerOption { OptionId = selectedOptionId }).ToList();
            }
            else
            {
                resultAnswer.TimeTakenSeconds = TimeSpan.Zero;
            }

            return resultAnswer;
        }

        private double CalculateQuestionScore(TestQuestion originalQuestion, PayloadQuestion receivedQuestion)
        {
            if (receivedQuestion != null)
            {
                var rightQuestionOptions = originalQuestion.TestQuestionOptions.Where(o => o.IsRight);
                int rightOptionsAmount = rightQuestionOptions.Count(), selectedRightOptionsAmount = 0;

                foreach (var id in receivedQuestion.SelectedOptionsId)
                {
                    selectedRightOptionsAmount = rightQuestionOptions.Any(o => o.Id == id) ? ++selectedRightOptionsAmount : --selectedRightOptionsAmount;
                }

                // Сalculate question score.
                return selectedRightOptionsAmount >= 0 ? (rightOptionsAmount == 0 && selectedRightOptionsAmount == 0 ? 1 : (double)selectedRightOptionsAmount / rightOptionsAmount) : 0;
            }

            return 0;
        }

        private bool IsResultAnswerInTime(TestQuestion testQuestion, ResultAnswer resultAnswer)
        {
            return resultAnswer.TimeTakenSeconds <= testQuestion.TimeLimitSeconds;
        }

        private bool IsAtLeastOneQuestionInTest(Test test)
        {
            return test.TestQuestions.Count > 0;
        }

        private bool IsTestResultInTime(Test test, TestResult testResult)
        {
            return (testResult.PassingEndTime - testResult.PassingStartTime) <= (test.TimeLimitSeconds + timeErrorSetting.MarginOfErrorSeconds);
        }

        private double GetPercentageTestResultScore(TestResult testResult)
        {
            return testResult.Score / testResult.ResultAnswers.Count * 100;
        }
    }
}
