using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class TestResult : EntityBase
    {
        public string IntervieweeName { get; set; }
        public DateTime PassingStartTime { get; set; }
        public DateTime PassingEndTime { get; set; }
        public int Score { get; set; }

        public virtual Url Url { get; set; }
        public virtual ICollection<ResultAnswer> ResultAnswers { get; set; } = new List<ResultAnswer>();
    }
}
