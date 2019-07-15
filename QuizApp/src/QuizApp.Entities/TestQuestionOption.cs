using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    public class TestQuestionOption
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsRight { get; set; }
        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public TestQuestion Question { get; set; }
        public ICollection<ResultAnswerOption> ResultAnswerOptions { get; set; } = new List<ResultAnswerOption>();
    }
}
