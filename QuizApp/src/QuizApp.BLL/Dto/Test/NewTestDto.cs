using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.BLL.Dto.Url;

namespace QuizApp.BLL.Dto.Test
{
    public class NewTestDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public int AuthorId { get; set; }


        public ICollection<NewTestQuestionDto> TestQuestions { get; set; } = new List<NewTestQuestionDto>();

        public ICollection<NewUrlDto> Urls { get; set; } = new List<NewUrlDto>();
    }
}
