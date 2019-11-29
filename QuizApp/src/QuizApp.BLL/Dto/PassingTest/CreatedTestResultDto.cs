using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.PassingTest
{
    public class CreatedTestResultDto
    {
        public int Id { get; set; }

        public string IntervieweeName { get; set; }

        public DateTime PassingStartTime { get; set; }

        public DateTime PassingEndTime { get; set; }

        public double Score { get; set; }


        public ICollection<CreatedResultAnswerDto> ResultAnswers { get; set; } = new List<CreatedResultAnswerDto>();
    }
}
