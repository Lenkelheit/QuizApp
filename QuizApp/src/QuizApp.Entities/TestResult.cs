using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    public class TestResult
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(length: EntitiesConstraints.INTERVIEWEE_NAME_MAX_LENGTH)]
        public string IntervieweeName { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime PassingStartTime { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime PassingEndTime { get; set; }

        public int Score { get; set; }

        public int UrlId { get; set; }


        [ForeignKey(nameof(UrlId))]
        public Url Url { get; set; }

        public ICollection<ResultAnswer> ResultAnswers { get; set; } = new List<ResultAnswer>();
    }
}
