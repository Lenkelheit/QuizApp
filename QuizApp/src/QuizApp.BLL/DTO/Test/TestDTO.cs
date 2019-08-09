using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.DTO.Test
{
    public class TestDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
