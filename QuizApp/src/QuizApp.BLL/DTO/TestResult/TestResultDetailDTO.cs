using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.DTO.ResultAnswer;

namespace QuizApp.BLL.DTO.TestResult
{
    public class TestResultDetailDTO
    {
        public int Id { get; set; }

        public string IntervieweeName { get; set; }

        public DateTime PassingStartTime { get; set; }

        public DateTime PassingEndTime { get; set; }

        public int Score { get; set; }


        public ICollection<ResultAnswerDetailDTO> ResultAnswers { get; set; }
    }
}
