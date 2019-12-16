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

        private readonly ITestCalculationService testCalculationService;

        private readonly IMapper mapper;

        private readonly TimeErrorSetting timeErrorSetting;


        public PassingTestService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<TimeErrorSetting> timeErrorSetting, ITestCalculationService testCalculationService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.testRepository = unitOfWork.GetRepository<Test, ITestRepository>() ?? throw new NullReferenceException(nameof(testRepository));
            this.testEventRepository = unitOfWork.GetRepository<TestEvent, ITestEventRepository>() ?? throw new NullReferenceException(nameof(testEventRepository));
            this.testResultRepository = unitOfWork.GetRepository<TestResult, ITestResultRepository>() ?? throw new NullReferenceException(nameof(testResultRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.timeErrorSetting = timeErrorSetting?.Value ?? throw new ArgumentNullException(nameof(timeErrorSetting));
            this.testCalculationService = testCalculationService ?? throw new ArgumentNullException(nameof(testCalculationService));
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

                testResult.Score += testCalculationService.IsResultAnswerInTime(question, resultAnswer)
                                    ? testCalculationService.CalculateQuestionScore(question, payloadQuestionIndex >= 0 ? payloadQuestions[payloadQuestionIndex] : null)
                                    : 0;
            }

            testResult.Score = testCalculationService.IsAtLeastOneQuestionInTest(test) && testCalculationService.IsTestResultInTime(test, testResult, timeErrorSetting)
                               ? testCalculationService.GetPercentageTestResultScore(testResult)
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
    }
}
