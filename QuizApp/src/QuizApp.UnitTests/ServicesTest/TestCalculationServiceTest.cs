using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.TestEvent.Payloads;
using QuizApp.BLL.Interfaces;
using QuizApp.BLL.Services;
using QuizApp.Entities;
using TestResult = QuizApp.Entities.TestResult;
using QuizApp.BLL.Settings;

namespace QuizApp.UnitTests.ServicesTest
{
    [TestClass]
    public class TestCalculationServiceTest
    {
        private readonly ITestCalculationService testCalculationService = new TestCalculationService();

        private readonly TestQuestion originalQuestion = new TestQuestion
        {
            TimeLimitSeconds = TimeSpan.FromHours(2),
            TestQuestionOptions = new List<TestQuestionOption>
            {
                    new TestQuestionOption { Id = 1, IsRight = true },
                    new TestQuestionOption { Id = 2, IsRight = false},
                    new TestQuestionOption { Id = 3, IsRight = true }
            }
        };

        private readonly TimeErrorSetting timeErrorSetting = new TimeErrorSetting { MarginOfErrorSeconds = TimeSpan.FromSeconds(2) };


        [TestMethod]
        public void CalculateQuestionScore_ReceivedQuestionIsNull_ReturnsZero()
        {
            var originalQuestion = new TestQuestion();

            var questionScore = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: null);

            Assert.AreEqual(expected: 0, actual: questionScore);
        }

        [TestMethod]
        public void CalculateQuestionScore_OriginalQuestionIsNull_ThrowsArgumentNullException()
        {
            var receivedQuestion = new PayloadQuestion();

            Assert.ThrowsException<ArgumentNullException>(() =>
                testCalculationService.CalculateQuestionScore(originalQuestion: null, receivedQuestion: receivedQuestion));
        }

        [TestMethod]
        public void CalculateQuestionScore_NotAllRightOptionsSelected_ReturnsNotMaximumScore()
        {
            var receivedQuestion = new PayloadQuestion
            {
                SelectedOptionsId = new List<int> { 1, 2 }
            };
            var expectedQuestionScore = 0;

            var actualQuestionScore = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: receivedQuestion);

            Assert.AreEqual(expected: expectedQuestionScore, actual: actualQuestionScore);
        }

        [TestMethod]
        public void CalculateQuestionScore_AllOptionsSelectedWithWrong_ReturnsNotMaximumScore()
        {
            var receivedQuestion = new PayloadQuestion
            {
                SelectedOptionsId = new List<int> { 1, 2, 3 }
            };
            var expectedQuestionScore = 1.0 / originalQuestion.TestQuestionOptions.Count(o => o.IsRight);

            var actualQuestionScore = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: receivedQuestion);

            Assert.AreEqual(expected: expectedQuestionScore, actual: actualQuestionScore);
        }

        [TestMethod]
        public void CalculateQuestionScore_MoreThanAllOptionsSelected_ReturnsNotMaximumScore()
        {
            var receivedQuestion = new PayloadQuestion
            {
                SelectedOptionsId = new List<int> { 1, 2, 3, 4 }
            };
            var expectedQuestionScore = 0;

            var actualQuestionScore = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: receivedQuestion);

            Assert.AreEqual(expected: expectedQuestionScore, actual: actualQuestionScore);
        }

        [TestMethod]
        public void CalculateQuestionScore_OnlyAllRightOptionsSelected_ReturnsMaximumScore()
        {
            var receivedQuestion = new PayloadQuestion
            {
                SelectedOptionsId = new List<int> { 1, 3 }
            };
            var expectedQuestionScore = 2.0 / originalQuestion.TestQuestionOptions.Count(o => o.IsRight);

            var actualQuestionScore = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: receivedQuestion);

            Assert.AreEqual(expected: expectedQuestionScore, actual: actualQuestionScore);
        }

        [TestMethod]
        public void CalculateQuestionScore_AllWrongOptionsSelected_ReturnsZero()
        {
            var receivedQuestion = new PayloadQuestion { SelectedOptionsId = new List<int> { 2 } };
            var expectedQuestionScore = 0;

            var actualQuestionScore = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: receivedQuestion);

            Assert.AreEqual(expected: expectedQuestionScore, actual: actualQuestionScore);
        }

        [TestMethod]
        public void CalculateQuestionScore_NoneOptionsSelectedAndQuestionOptionsAreNotEmpty_ReturnsZero()
        {
            var receivedQuestion = new PayloadQuestion { SelectedOptionsId = new List<int> { } };
            var expectedQuestionScore = 0;

            var actualQuestionScore = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: receivedQuestion);

            Assert.AreEqual(expected: expectedQuestionScore, actual: actualQuestionScore);
        }

        [TestMethod]
        public void CalculateQuestionScore_NoneOptionsSelectedAndQuestionOptionsAreEmpty_ReturnsMaximumScore()
        {
            var originalQuestion = new TestQuestion { TestQuestionOptions = new List<TestQuestionOption> { } };
            var receivedQuestion = new PayloadQuestion { SelectedOptionsId = new List<int> { } };
            var expectedQuestionScore = 1;

            var actualQuestionScore = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: receivedQuestion);

            Assert.AreEqual(expected: expectedQuestionScore, actual: actualQuestionScore);
        }

        [TestMethod]
        public void IsResultAnswerInTime_AnswerTimeIsMore_ReturnsFalse()
        {
            var resultAnswer = new ResultAnswer { TimeTakenSeconds = originalQuestion.TimeLimitSeconds + TimeSpan.FromHours(1) };

            var isResultAnswerInTime = testCalculationService.IsResultAnswerInTime(testQuestion: originalQuestion, resultAnswer: resultAnswer);

            Assert.IsFalse(isResultAnswerInTime);
        }

        [TestMethod]
        public void IsResultAnswerInTime_AnswerTimeIsTheSame_ReturnsTrue()
        {
            var resultAnswer = new ResultAnswer { TimeTakenSeconds = originalQuestion.TimeLimitSeconds };

            var isResultAnswerInTime = testCalculationService.IsResultAnswerInTime(testQuestion: originalQuestion, resultAnswer: resultAnswer);

            Assert.IsTrue(isResultAnswerInTime);
        }

        [TestMethod]
        public void IsResultAnswerInTime_AnswerTimeIsLess_ReturnsTrue()
        {
            var resultAnswer = new ResultAnswer { TimeTakenSeconds = originalQuestion.TimeLimitSeconds - TimeSpan.FromHours(1) };

            var isResultAnswerInTime = testCalculationService.IsResultAnswerInTime(testQuestion: originalQuestion, resultAnswer: resultAnswer);

            Assert.IsTrue(isResultAnswerInTime);
        }

        [TestMethod]
        public void IsResultAnswerInTime_ResultAnswerIsNull_ThrowsArgumentNullException()
        {
            var testQuestion = new TestQuestion();

            Assert.ThrowsException<ArgumentNullException>(() =>
                testCalculationService.IsResultAnswerInTime(testQuestion: testQuestion, resultAnswer: null));
        }

        [TestMethod]
        public void IsResultAnswerInTime_TestQuestionIsNull_ThrowsArgumentNullException()
        {
            var resultAnswer = new ResultAnswer();

            Assert.ThrowsException<ArgumentNullException>(() =>
                testCalculationService.IsResultAnswerInTime(testQuestion: null, resultAnswer: resultAnswer));
        }

        [TestMethod]
        public void IsAtLeastOneQuestionInTest_TestIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                testCalculationService.IsAtLeastOneQuestionInTest(test: null));
        }

        [TestMethod]
        public void IsAtLeastOneQuestionInTest_TestContainsQuestions_ReturnsTrue()
        {
            var test = new Test
            {
                TestQuestions = new List<TestQuestion>
                {
                    new TestQuestion { }, new TestQuestion{ }, new TestQuestion{ }
                }
            };

            var isAtLeastOneQuestionInTest = testCalculationService.IsAtLeastOneQuestionInTest(test);

            Assert.IsTrue(isAtLeastOneQuestionInTest);
        }

        [TestMethod]
        public void IsAtLeastOneQuestionInTest_TestNotContainQuestions_ReturnsFalse()
        {
            var test = new Test();

            var isAtLeastOneQuestionInTest = testCalculationService.IsAtLeastOneQuestionInTest(test);

            Assert.IsFalse(isAtLeastOneQuestionInTest);
        }

        [TestMethod]
        public void IsTestResultInTime_TestIsNull_ThrowsArgumentNullException()
        {
            var testResult = new TestResult();

            Assert.ThrowsException<ArgumentNullException>(() =>
                testCalculationService.IsTestResultInTime(test: null, testResult, timeErrorSetting));
        }

        [TestMethod]
        public void IsTestResultInTime_TestResultIsNull_ThrowsArgumentNullException()
        {
            var test = new Test();

            Assert.ThrowsException<ArgumentNullException>(() =>
                testCalculationService.IsTestResultInTime(test, testResult: null, timeErrorSetting));
        }

        [TestMethod]
        public void IsTestResultInTime_TimeErrorSettingIsNull_ThrowsArgumentNullException()
        {
            var test = new Test();
            var testResult = new TestResult();

            Assert.ThrowsException<ArgumentNullException>(() =>
                testCalculationService.IsTestResultInTime(test, testResult, timeErrorSetting: null));
        }

        [TestMethod]
        public void IsTestResultInTime_TestResultTimeIsMore_ReturnsFalse()
        {
            int testHour = 3, testResultHour = 5;
            var test = new Test { TimeLimitSeconds = TimeSpan.FromHours(testHour) };
            var testResult = new TestResult { PassingStartTime = new DateTime(1, 1, 1, 1, 1, 1), PassingEndTime = new DateTime(1, 1, 1, 1 + testResultHour, 1, 1) };

            var isTestResultInTime = testCalculationService.IsTestResultInTime(test, testResult, timeErrorSetting);

            Assert.IsFalse(isTestResultInTime);
        }

        [TestMethod]
        public void IsTestResultInTime_TestResultTimeIsTheSame_ReturnsTrue()
        {
            int testHour = 3;
            var test = new Test { TimeLimitSeconds = TimeSpan.FromHours(testHour) };
            var testResult = new TestResult { PassingStartTime = new DateTime(1, 1, 1, 1, 1, 1), PassingEndTime = new DateTime(1, 1, 1, 1 + testHour, 1, 1) };

            var isTestResultInTime = testCalculationService.IsTestResultInTime(test, testResult, timeErrorSetting);

            Assert.IsTrue(isTestResultInTime);
        }

        [TestMethod]
        public void IsTestResultInTime_TestResultTimeIsLess_ReturnsTrue()
        {
            int testHour = 3, testResultHour = 2;
            var test = new Test { TimeLimitSeconds = TimeSpan.FromHours(testHour) };
            var testResult = new TestResult { PassingStartTime = new DateTime(1, 1, 1, 1, 1, 1), PassingEndTime = new DateTime(1, 1, 1, 1 + testResultHour, 1, 1) };

            var isTestResultInTime = testCalculationService.IsTestResultInTime(test, testResult, timeErrorSetting);

            Assert.IsTrue(isTestResultInTime);
        }

        [TestMethod]
        public void GetPercentageTestResultScore_TestResultIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                testCalculationService.GetPercentageTestResultScore(testResult: null));
        }

        [TestMethod]
        public void GetPercentageTestResultScore_IsScorePrecise_ReturnsPreciseScore()
        {
            var testResult = new TestResult
            {
                Score = 2,
                ResultAnswers = new List<ResultAnswer>
                {
                    new ResultAnswer(), new ResultAnswer(), new ResultAnswer()
                }
            };
            var expectedTestResultScore = testResult.Score / testResult.ResultAnswers.Count * 100.0;

            var actualTestResultScore = testCalculationService.GetPercentageTestResultScore(testResult);

            Assert.AreEqual(expected: expectedTestResultScore, actual: actualTestResultScore);
        }
    }
}
