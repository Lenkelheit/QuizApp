using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class TestQuestion : EntityBase
    {
        public string Text { get; set; }
        public string Hint { get; set; }
        public TimeSpan TimeLimitSeconds { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<TestQuestionOption> TestQuestionOptions { get; set; } = new List<TestQuestionOption>();
        public virtual ICollection<ResultAnswer> ResultAnswers { get; set; } = new List<ResultAnswer>();
    }
}
