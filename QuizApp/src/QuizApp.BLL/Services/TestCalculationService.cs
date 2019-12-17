using System;
using System.Collections.Generic;
using System.Linq;

using QuizApp.BLL.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.Dto.TestEvent.Payloads;

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
    }
}
