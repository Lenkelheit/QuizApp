using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class ResultAnswer : EntityBase
    {
        public TimeSpan TakeTimeSeconds { get; set; }

        public virtual TestResult Result { get; set; }
        public int? QuestionId { get; set; }
        public virtual TestQuestion Question { get; set; }
        public virtual ICollection<ResultAnswerOption> ResultAnswerOptions { get; set; } = new List<ResultAnswerOption>();
    }
}
