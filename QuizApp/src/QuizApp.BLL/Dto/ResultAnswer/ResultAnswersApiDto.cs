using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.ResultAnswer
{
    public class ResultAnswersApiDto
    {
        public int TotalCount { get; set; }


        public ICollection<ResultAnswerFromResultDto> ResultAnswers { get; set; } = new List<ResultAnswerFromResultDto>();
    }
}
