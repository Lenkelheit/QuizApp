using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.BLL.Dto.Url;

namespace QuizApp.BLL.Dto.Test
{
    public class TestDetailDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public int AuthorId { get; set; }


        public ICollection<UrlDetailDto> Urls { get; set; } = new List<UrlDetailDto>();

        public ICollection<TestQuestionDetailDto> TestQuestions { get; set; } = new List<TestQuestionDetailDto>();
    }
}
