using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.Test;

namespace QuizApp.BLL.Dto.TestResult
{
    public class TestResultDetailDto
    {
        public int Id { get; set; }

        public string IntervieweeName { get; set; }

        public DateTime PassingStartTime { get; set; }

        public DateTime PassingEndTime { get; set; }

        public double Score { get; set; }


        public ResultTestDto Test { get; set; }
    }
}
