using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class TestResult
    {
        public int Id { get; set; }
        public string IntervieweeName { get; set; }
        public DateTime PassingStartTime { get; set; }
        public DateTime PassingEndTime { get; set; }
        public int Score { get; set; }

        public Url Url { get; set; }
        public ICollection<ResultAnswer> ResultAnswers { get; set; } = new List<ResultAnswer>();
    }
}
