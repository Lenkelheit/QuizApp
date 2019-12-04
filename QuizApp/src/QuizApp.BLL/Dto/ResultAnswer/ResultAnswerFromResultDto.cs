using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.ResultAnswerOption;
using QuizApp.BLL.Dto.TestQuestion;

namespace QuizApp.BLL.Dto.ResultAnswer
{
    public class ResultAnswerFromResultDto
    {
        public int Id { get; set; }

        public TimeSpan TimeTakenSeconds { get; set; }


        public ResultQuestionDto Question { get; set; }

        public ICollection<ResultAnswerOptionDetailDto> ResultAnswerOptions { get; set; } = new List<ResultAnswerOptionDetailDto>();
    }
}
