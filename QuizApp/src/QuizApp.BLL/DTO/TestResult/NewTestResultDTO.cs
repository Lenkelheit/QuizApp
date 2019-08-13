using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.TestResult
{
    public class NewTestResultDto
    {
        public string IntervieweeName { get; set; }

        public DateTime PassingStartTime { get; set; }

        public DateTime PassingEndTime { get; set; }

        public int Score { get; set; }

        public int UrlId { get; set; }
    }
}
