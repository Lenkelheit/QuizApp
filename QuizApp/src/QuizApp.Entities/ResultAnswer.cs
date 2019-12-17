using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class ResultAnswer : IEntity<int>
    {
        public int Id { get; set; }

        public TimeSpan TimeTakenSeconds { get; set; }

        public int? QuestionId { get; set; }

        public int ResultId { get; set; }


        public TestQuestion Question { get; set; }

        public TestResult Result { get; set; }

        public ICollection<ResultAnswerOption> ResultAnswerOptions { get; set; } = new List<ResultAnswerOption>();


        public bool IsInTime(TestQuestion testQuestion)
        {
            if (testQuestion == null) throw new ArgumentNullException(nameof(testQuestion));

            return TimeTakenSeconds <= testQuestion.TimeLimitSeconds;
        }
    }
}
