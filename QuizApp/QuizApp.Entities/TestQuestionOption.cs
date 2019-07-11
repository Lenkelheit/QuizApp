using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class TestQuestionOption : EntityBase
    {
        public string Text { get; set; }
        public bool IsRight { get; set; }

        public virtual TestQuestion Question { get; set; }
        public virtual ICollection<ResultAnswerOption> ResultAnswerOptions { get; set; } = new List<ResultAnswerOption>();
    }
}
