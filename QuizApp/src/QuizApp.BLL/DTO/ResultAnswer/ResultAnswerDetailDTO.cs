using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.ResultAnswerOption;

namespace QuizApp.BLL.Dto.ResultAnswer
{
    public class ResultAnswerDetailDto
    {
        public int Id { get; set; }

        public TimeSpan TimeTakenSeconds { get; set; }


        public ICollection<ResultAnswerOptionDetailDto> ResultAnswerOptions { get; set; } = new List<ResultAnswerOptionDetailDto>();
    }
}
