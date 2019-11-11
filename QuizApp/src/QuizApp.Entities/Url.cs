using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class Url : IEntity<int>
    {
        public int Id { get; set; }

        public int? NumberOfRuns { get; set; }

        public DateTime ValidFromTime { get; set; }

        public DateTime ValidUntilTime { get; set; }

        public string IntervieweeName { get; set; }

        public int? TestId { get; set; }


        public Test Test { get; set; }

        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}
