using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.TestQuestion
{
    public class CreatedTestQuestionDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Hint { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }
    }
}
