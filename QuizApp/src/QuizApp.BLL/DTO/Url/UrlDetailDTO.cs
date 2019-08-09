using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.DTO.TestResult;

namespace QuizApp.BLL.DTO.Url
{
    public class UrlDetailDTO
    {
        public int Id { get; set; }

        public int? NumberOfRuns { get; set; }

        public DateTime ValidFromTime { get; set; }

        public DateTime ValidUntilTime { get; set; }

        public string IntervieweeName { get; set; }


        public ICollection<TestResultDetailDTO> TestResults { get; set; }
    }
}
