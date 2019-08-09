using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.DTO.ResultAnswerOption;

namespace QuizApp.BLL.DTO.TestQuestionOption
{
    public class TestQuestionOptionDetailDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsRight { get; set; }


        public ICollection<ResultAnswerOptionDetailDTO> ResultAnswerOptions { get; set; }
    }
}
