using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.PassingTest
{
    public class ViewQuestionDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Hint { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }


        public ICollection<ViewQuestionOptionDto> TestQuestionOptions { get; set; } = new List<ViewQuestionOptionDto>();
    }
}
