using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    [Table(nameof(TestQuestion))]
    public class TestQuestion
    {
        public const int TextQuestionMaxLength = 512;

        public const int HintMaxLength = 256;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(length: TextQuestionMaxLength)]
        public string Text { get; set; }

        [MaxLength(length: HintMaxLength)]
        public string Hint { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan TimeLimitSeconds { get; set; }

        public int TestId { get; set; }


        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }

        public ICollection<TestQuestionOption> TestQuestionOptions { get; set; } = new List<TestQuestionOption>();

        public ICollection<ResultAnswer> ResultAnswers { get; set; } = new List<ResultAnswer>();
    }
}
