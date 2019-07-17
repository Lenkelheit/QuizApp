using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class TestQuestion
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Hint { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public int TestId { get; set; }


        public Test Test { get; set; }

        public ICollection<TestQuestionOption> TestQuestionOptions { get; set; } = new List<TestQuestionOption>();

        public ICollection<ResultAnswer> ResultAnswers { get; set; } = new List<ResultAnswer>();
    }
}
