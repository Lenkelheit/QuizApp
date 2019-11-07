using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AutoMapper;
using Newtonsoft.Json;
using FluentValidation.Results;

using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.Dto.PassingTest;
using QuizApp.BLL.Validators.PassingTest;
using QuizApp.Entities.Enums;
using QuizApp.BLL.Dto.TestEvent.Payloads;
using QuizApp.BLL.Settings;

namespace QuizApp.BLL.Services
{
    public class PassingTestService : IPassingTestService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ITestRepository testRepository;

        private readonly IUrlRepository urlRepository;

        private readonly ITestEventRepository testEventRepository;

        private readonly ITestResultRepository testResultRepository;

        private readonly IMapper mapper;

        private readonly TimeErrorSetting timeErrorSetting;


        public PassingTestService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<TimeErrorSetting> timeErrorSetting)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.testRepository = unitOfWork.GetRepository<Test, ITestRepository>() ?? throw new NullReferenceException(nameof(testRepository));
            this.urlRepository = unitOfWork.GetRepository<Url, IUrlRepository>() ?? throw new NullReferenceException(nameof(urlRepository));
            this.testEventRepository = unitOfWork.GetRepository<TestEvent, ITestEventRepository>() ?? throw new NullReferenceException(nameof(testEventRepository));
            this.testResultRepository = unitOfWork.GetRepository<TestResult, ITestResultRepository>() ?? throw new NullReferenceException(nameof(testResultRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.timeErrorSetting = timeErrorSetting?.Value ?? throw new ArgumentNullException(nameof(timeErrorSetting));
        }


        public async Task<UrlValidationResultDto> CheckIsUrlValid(int urlId)
        {
            Url url = await urlRepository.GetByIdAsync(id: urlId);

            UrlValidator urlValidator = new UrlValidator();
            ValidationResult result = urlValidator.Validate(url);

            return mapper.Map<UrlValidationResultDto>(result);
        }

        public async Task<UserIdentificationResultDto> IdentifyUser(IdentityUrlDto urlDto)
        {
            Url url = await urlRepository.GetByIdAsync(id: urlDto.Id);

            UrlValidator urlValidator = new UrlValidator();
            ValidationResult urlValidationResult = urlValidator.Validate(url);

            UserIdentificationResultDto userIdentificationResult;

            if (!urlValidationResult.IsValid)
            {
                userIdentificationResult = mapper.Map<UserIdentificationResultDto>(urlValidationResult);
                userIdentificationResult.IsUrlValid = false;
                return userIdentificationResult;
            }

            if (url.NumberOfRuns.HasValue && url.NumberOfRuns > 0)
            {
                --url.NumberOfRuns;
                urlRepository.Update(url);
                await unitOfWork.SaveAsync();
            }

            UrlIntervieweeNameValidator intervieweeNameValidator = new UrlIntervieweeNameValidator(urlDto.IntervieweeName);
            ValidationResult intervieweeNameValidationResult = intervieweeNameValidator.Validate(url);

            userIdentificationResult = mapper.Map<UserIdentificationResultDto>(intervieweeNameValidationResult);
            userIdentificationResult.IsUrlValid = true;
            return userIdentificationResult;
        }

        public async Task<ViewTestDto> GetTestById(int testId)
        {
            Test test = await testRepository.GetByIdAsync(id: testId, includeProperties: prop => prop
                .Include(t => t.TestQuestions)
                    .ThenInclude(q => q.TestQuestionOptions));

            return mapper.Map<ViewTestDto>(test);
        }

        public async Task<CreatedTestResultDto> CreateTestResult(UserUrlDto userUrlDto)
        {
            IEnumerable<TestEvent> testEvents = testEventRepository.Get(e => e.SessionId == userUrlDto.SessionId);

            List<TestEvent> eventsQuestionAnswered = testEvents.Where(e => e.EventType == EventType.QuestionAnswered).ToList();
            List<PayloadQuestion> payloadQuestions = new List<PayloadQuestion>();
            eventsQuestionAnswered.ForEach(eventQuestion => payloadQuestions.Add(JsonConvert.DeserializeObject<PayloadQuestion>(eventQuestion.Payload)));

            TestEvent eventTestStarted = testEvents.First(e => e.EventType == EventType.TestStarted);
            PayloadTest payloadTest = JsonConvert.DeserializeObject<PayloadTest>(eventTestStarted.Payload);
            Test test = testRepository.GetById(id: payloadTest.TestId, includeProperties: prop => prop.Include(t => t.TestQuestions).ThenInclude(q => q.TestQuestionOptions));

            TestResult testResult = new TestResult()
            {
                IntervieweeName = payloadTest.IntervieweeName,
                PassingStartTime = eventTestStarted.StartTime,
                PassingEndTime = DateTime.Now,
                UrlId = userUrlDto.UrlId
            };

            foreach (TestQuestion question in test.TestQuestions)
            {
                int payloadQuestionIndex = payloadQuestions.FindIndex(q => q.QuestionId == question.Id);

                ResultAnswer resultAnswer = CreateResultAnswerWithOptions(payloadQuestionIndex >= 0 ? payloadQuestions[payloadQuestionIndex] : null,
                                                                          payloadQuestionIndex,
                                                                          eventsQuestionAnswered,
                                                                          eventTestStarted);
                resultAnswer.QuestionId = question.Id;

                testResult.ResultAnswers.Add(resultAnswer);

                testResult.Score += resultAnswer.TimeTakenSeconds <= question.TimeLimitSeconds
                                    ? CalculateQuestionScore(question, payloadQuestionIndex >= 0 ? payloadQuestions[payloadQuestionIndex] : null) : 0;
            }

            testResult.Score = (test.TestQuestions.Count != 0
                               && (testResult.PassingEndTime - testResult.PassingStartTime) <= (test.TimeLimitSeconds + timeErrorSetting.MarginOfErrorSeconds))
                               ? testResult.Score / test.TestQuestions.Count * 100 : 0;

            testEventRepository.Delete(testEvents);
            testResultRepository.Insert(testResult);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestResultDto>(testResult);
        }

        private ResultAnswer CreateResultAnswerWithOptions(PayloadQuestion receivedQuestion, int receivedQuestionIndex, List<TestEvent> eventsQuestionAnswered, TestEvent eventTestStarted)
        {
            ResultAnswer resultAnswer = new ResultAnswer();

            if (receivedQuestionIndex >= 0)
            {
                resultAnswer.TimeTakenSeconds = receivedQuestionIndex == 0 ?
                    eventsQuestionAnswered[receivedQuestionIndex].StartTime - eventTestStarted.StartTime :
                    eventsQuestionAnswered[receivedQuestionIndex].StartTime - eventsQuestionAnswered[receivedQuestionIndex - 1].StartTime;

                foreach (int selectedOptionId in receivedQuestion.SelectedOptionsId)
                {
                    resultAnswer.ResultAnswerOptions.Add(new ResultAnswerOption { OptionId = selectedOptionId });
                }
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
                IEnumerable<TestQuestionOption> rightQuestionOptions = originalQuestion.TestQuestionOptions.Where(o => o.IsRight);
                int rightOptionsAmount = rightQuestionOptions.Count(), selectedRightOptionsAmount = 0;

                receivedQuestion.SelectedOptionsId.ToList()
                    .ForEach(id => selectedRightOptionsAmount = rightQuestionOptions.Any(o => o.Id == id) ? ++selectedRightOptionsAmount : --selectedRightOptionsAmount);

                // calculate question score
                return selectedRightOptionsAmount >= 0 ? (rightOptionsAmount == 0 && selectedRightOptionsAmount == 0 ? 1 : (double)selectedRightOptionsAmount / rightOptionsAmount) : 0;
            }

            return 0;
        }
    }
}
