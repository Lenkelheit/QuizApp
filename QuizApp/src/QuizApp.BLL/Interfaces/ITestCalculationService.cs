using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.TestEvent.Payloads;
using QuizApp.Entities;

namespace QuizApp.BLL.Interfaces
{
    public interface ITestCalculationService
    {
        double CalculateQuestionScore(TestQuestion originalQuestion, PayloadQuestion receivedQuestion);
    }
}
