using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.DTO.Url
{
    public class UrlDTO
    {
        public int Id { get; set; }

        public int? NumberOfRuns { get; set; }

        public DateTime ValidFromTime { get; set; }

        public DateTime ValidUntilTime { get; set; }

        public string IntervieweeName { get; set; }
    }
}
