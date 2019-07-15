using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    public class Url
    {
        public int Id { get; set; }
        public int? NumberOfRuns { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime ValidFromTime { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime ValidUntilTime { get; set; }
        public string IntervieweeName { get; set; }
        public int TestId { get; set; }

        public Test Test { get; set; }
        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}
