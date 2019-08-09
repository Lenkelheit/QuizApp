using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.DTO.TestResult
{
    public class NewTestResultDTO
    {
        public string IntervieweeName { get; set; }

        public DateTime PassingStartTime { get; set; }

        public DateTime PassingEndTime { get; set; }

        public int Score { get; set; }

        public int UrlId { get; set; }
    }
}
