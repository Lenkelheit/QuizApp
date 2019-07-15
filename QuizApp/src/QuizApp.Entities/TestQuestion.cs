using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    public class TestQuestion
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(length: EntitiesConstraints.TEXT_QUESTION_MAX_LENGTH)]
        public string Text { get; set; }

        [MaxLength(length: EntitiesConstraints.HINT_MAX_LENGTH)]
        public string Hint { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan TimeLimitSeconds { get; set; }

        public int TestId { get; set; }


        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }

        public ICollection<TestQuestionOption> TestQuestionOptions { get; set; } = new List<TestQuestionOption>();

        public ICollection<ResultAnswer> ResultAnswers { get; set; } = new List<ResultAnswer>();
    }
}
