using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class TestQuestionOption
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsRight { get; set; }

        public int QuestionId { get; set; }


        public TestQuestion Question { get; set; }

        public ICollection<ResultAnswerOption> ResultAnswerOptions { get; set; } = new List<ResultAnswerOption>();
    }
}
