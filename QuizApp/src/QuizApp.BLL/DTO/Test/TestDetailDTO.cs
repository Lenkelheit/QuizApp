using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.DTO.TestQuestion;
using QuizApp.BLL.DTO.Url;

namespace QuizApp.BLL.DTO.Test
{
    public class TestDetailDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public DateTime LastModifiedDate { get; set; }


        public ICollection<UrlDetailDTO> Urls { get; set; }

        public ICollection<TestQuestionDetailDTO> TestQuestions { get; set; }
    }
}
