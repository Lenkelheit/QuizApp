using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.Test
{
    public class DeletedTestDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
