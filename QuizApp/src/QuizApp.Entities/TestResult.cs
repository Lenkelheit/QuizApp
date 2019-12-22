using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class TestResult : IEntity<int>
    {
        public int Id { get; set; }

        public string IntervieweeName { get; set; }

        public DateTime PassingStartTime { get; set; }

        public DateTime PassingEndTime { get; set; }

        public double Score { get; set; }

        public int UrlId { get; set; }

        public double PercentageScore => Score / ResultAnswers.Count * 100.0;


        public Url Url { get; set; }

        public ICollection<ResultAnswer> ResultAnswers { get; set; } = new List<ResultAnswer>();


        public bool IsInTime(Test test, TimeSpan marginOfErrorSeconds)
        {
            if (test == null) throw new ArgumentNullException(nameof(test));

            return (PassingEndTime - PassingStartTime) <= (test.TimeLimitSeconds + marginOfErrorSeconds);
        }
    }
}
