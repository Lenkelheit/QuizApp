using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    [Table(nameof(TestQuestionOption))]
    public class TestQuestionOption
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(length: 256)]
        public string Text { get; set; }

        public bool IsRight { get; set; }

        public int QuestionId { get; set; }


        [ForeignKey(nameof(QuestionId))]
        public TestQuestion Question { get; set; }

        public ICollection<ResultAnswerOption> ResultAnswerOptions { get; set; } = new List<ResultAnswerOption>();
    }
}
