using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    [Table(nameof(TestResult))]
    public class TestResult
    {
        public const int IntervieweeNameMaxLength = 128;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(length: IntervieweeNameMaxLength)]
        public string IntervieweeName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PassingStartTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PassingEndTime { get; set; }

        public int Score { get; set; }

        public int UrlId { get; set; }


        [ForeignKey(nameof(UrlId))]
        public Url Url { get; set; }

        public ICollection<ResultAnswer> ResultAnswers { get; set; } = new List<ResultAnswer>();
    }
}
