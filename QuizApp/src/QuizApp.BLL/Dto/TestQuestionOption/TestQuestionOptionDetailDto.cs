using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.ResultAnswerOption;

namespace QuizApp.BLL.Dto.TestQuestionOption
{
    public class TestQuestionOptionDetailDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsRight { get; set; }


        public ICollection<ResultAnswerOptionDetailDto> ResultAnswerOptions { get; set; } = new List<ResultAnswerOptionDetailDto>();
    }
}
