using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    [Table(nameof(Url))]
    public class Url
    {
        [Key]
        public int Id { get; set; }

        public int? NumberOfRuns { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ValidFromTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ValidUntilTime { get; set; }

        [MaxLength(length: 128)]
        public string IntervieweeName { get; set; }

        public int TestId { get; set; }


        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }

        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}
