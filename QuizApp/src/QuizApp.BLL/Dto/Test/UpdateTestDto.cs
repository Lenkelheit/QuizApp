using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.TestQuestion;

namespace QuizApp.BLL.Dto.Test
{
    public class UpdateTestDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public int AuthorId { get; set; }


        public ICollection<UpdateTestQuestionDto> TestQuestions { get; set; } = new List<UpdateTestQuestionDto>();
    }
}
