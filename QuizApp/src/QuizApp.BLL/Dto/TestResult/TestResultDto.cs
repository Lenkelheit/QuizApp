using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.TestResult
{
    public class TestResultDto
    {
        public int Id { get; set; }

        public string IntervieweeName { get; set; }

        public DateTime PassingStartTime { get; set; }

        public DateTime PassingEndTime { get; set; }

        public double Score { get; set; }
    }
}
