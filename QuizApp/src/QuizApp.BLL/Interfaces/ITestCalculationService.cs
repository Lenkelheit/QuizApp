using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.TestEvent.Payloads;
using QuizApp.BLL.Settings;
using QuizApp.Entities;

namespace QuizApp.BLL.Interfaces
{
    public interface ITestCalculationService
    {
        double CalculateQuestionScore(TestQuestion originalQuestion, PayloadQuestion receivedQuestion);

        bool IsResultAnswerInTime(TestQuestion testQuestion, ResultAnswer resultAnswer);

        bool IsAtLeastOneQuestionInTest(Test test);

        bool IsTestResultInTime(Test test, TestResult testResult, TimeErrorSetting timeErrorSetting);

        double GetPercentageTestResultScore(TestResult testResult);
    }
}
