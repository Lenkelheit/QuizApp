using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.ResultAnswer;

namespace QuizApp.BLL.Dto.TestResult
{
    public class TestResultDetailDto
    {
        public int Id { get; set; }

        public string IntervieweeName { get; set; }

        public DateTime PassingStartTime { get; set; }

        public DateTime PassingEndTime { get; set; }

        public int Score { get; set; }


        public ICollection<ResultAnswerDetailDto> ResultAnswers { get; set; } = new List<ResultAnswerDetailDto>();
    }
}
