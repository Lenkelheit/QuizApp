using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.DTO.ResultAnswer;
using QuizApp.BLL.DTO.TestQuestionOption;

namespace QuizApp.BLL.DTO.TestQuestion
{
    public class TestQuestionDetailDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Hint { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }


        public ICollection<TestQuestionOptionDetailDTO> TestQuestionOptions { get; set; }

        public ICollection<ResultAnswerDetailDTO> ResultAnswers { get; set; }
    }
}
