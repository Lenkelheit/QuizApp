using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.TestQuestionOption;

namespace QuizApp.BLL.Dto.TestQuestion
{
    public class NewTestQuestionDto
    {
        public string Text { get; set; }

        public string Hint { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }


        public ICollection<NewTestQuestionOptionDto> TestQuestionOptions { get; set; } = new List<NewTestQuestionOptionDto>();
    }
}
