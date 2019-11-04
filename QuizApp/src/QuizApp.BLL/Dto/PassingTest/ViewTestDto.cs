using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.PassingTest
{
    public class ViewTestDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public int AuthorId { get; set; }


        public ICollection<ViewQuestionDto> TestQuestions { get; set; } = new List<ViewQuestionDto>();
    }
}
