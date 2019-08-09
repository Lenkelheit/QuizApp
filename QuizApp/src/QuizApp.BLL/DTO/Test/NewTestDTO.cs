using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.DTO.Test
{
    public class NewTestDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public int AuthorId { get; set; }
    }
}
