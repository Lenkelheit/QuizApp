using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.TestQuestion
{
    public class NewTestQuestionDto
    {
        public string Text { get; set; }

        public string Hint { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public int TestId { get; set; }
    }
}
