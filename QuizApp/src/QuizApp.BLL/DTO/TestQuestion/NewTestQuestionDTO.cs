using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.DTO.TestQuestion
{
    public class NewTestQuestionDTO
    {
        public string Text { get; set; }

        public string Hint { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public int TestId { get; set; }
    }
}
