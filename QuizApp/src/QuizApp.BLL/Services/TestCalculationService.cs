using System;
using System.Collections.Generic;
using System.Linq;

using QuizApp.BLL.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.Dto.TestEvent.Payloads;
using QuizApp.BLL.Settings;

namespace QuizApp.BLL.Services
{
    public class TestCalculationService : ITestCalculationService
    {
        public double CalculateQuestionScore(TestQuestion originalQuestion, PayloadQuestion receivedQuestion)
        {
            if (originalQuestion == null) throw new ArgumentNullException(nameof(originalQuestion));

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

        public bool IsResultAnswerInTime(TestQuestion testQuestion, ResultAnswer resultAnswer)
        {
            if (testQuestion == null) throw new ArgumentNullException(nameof(testQuestion));
            if (resultAnswer == null) throw new ArgumentNullException(nameof(resultAnswer));

            return resultAnswer.TimeTakenSeconds <= testQuestion.TimeLimitSeconds;
        }

        public bool IsAtLeastOneQuestionInTest(Test test)
        {
            if (test == null) throw new ArgumentNullException(nameof(test));

            return test.TestQuestions.Count > 0;
        }

        public bool IsTestResultInTime(Test test, TestResult testResult, TimeErrorSetting timeErrorSetting)
        {
            if (test == null) throw new ArgumentNullException(nameof(test));
            if (testResult == null) throw new ArgumentNullException(nameof(testResult));
            if (timeErrorSetting == null) throw new ArgumentNullException(nameof(timeErrorSetting));

            return (testResult.PassingEndTime - testResult.PassingStartTime) <= (test.TimeLimitSeconds + timeErrorSetting.MarginOfErrorSeconds);
        }

        public double GetPercentageTestResultScore(TestResult testResult)
        {
            if (testResult == null) throw new ArgumentNullException(nameof(testResult));

            return testResult.Score / testResult.ResultAnswers.Count * 100.0;
        }
    }
}
