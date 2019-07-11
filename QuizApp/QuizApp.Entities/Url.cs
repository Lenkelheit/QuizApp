using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class Url : EntityBase
    {
        public int? NumberOfRuns { get; set; }
        public DateTime ValidFromTime { get; set; }
        public DateTime ValidUntilTime { get; set; }
        public string IntervieweeName { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}
