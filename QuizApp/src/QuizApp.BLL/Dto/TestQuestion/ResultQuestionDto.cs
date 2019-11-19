using QuizApp.BLL.Dto.TestQuestionOption;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.TestQuestion
{
    public class ResultQuestionDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Hint { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }


        public ICollection<ResultQuestionOptionDto> TestQuestionOptions { get; set; } = new List<ResultQuestionOptionDto>();
    }
}
