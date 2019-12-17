using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.TestEvent.Payloads;
using QuizApp.BLL.Interfaces;
using QuizApp.BLL.Services;
using QuizApp.Entities;

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


        [TestMethod]
        public void CalculateQuestionScore_ReceivedQuestionIsNull_ReturnsZero()
        {
            var originalQuestion = new TestQuestion();
            var expectedQuestionScore = 0;

            var actualQuestionScore = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: null);

            Assert.AreEqual(expected: expectedQuestionScore, actual: actualQuestionScore);
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
            var expectedQuestionScore = 1.0;
            var expectedQuestionScoreCoef = expectedQuestionScore / originalQuestion.TestQuestionOptions.Count(o => o.IsRight);

            var actualQuestionScoreCoef = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: receivedQuestion);

            Assert.AreEqual(expected: expectedQuestionScoreCoef, actual: actualQuestionScoreCoef);
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
            var expectedQuestionScore = 2.0;
            var expectedQuestionScoreCoef = expectedQuestionScore / originalQuestion.TestQuestionOptions.Count(o => o.IsRight);

            var actualQuestionScoreCoef = testCalculationService.CalculateQuestionScore(originalQuestion: originalQuestion, receivedQuestion: receivedQuestion);

            Assert.AreEqual(expected: expectedQuestionScoreCoef, actual: actualQuestionScoreCoef);
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
    }
}
