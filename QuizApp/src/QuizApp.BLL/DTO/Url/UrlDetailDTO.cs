using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.TestResult;

namespace QuizApp.BLL.Dto.Url
{
    public class UrlDetailDto
    {
        public int Id { get; set; }

        public int? NumberOfRuns { get; set; }

        public DateTime ValidFromTime { get; set; }

        public DateTime ValidUntilTime { get; set; }

        public string IntervieweeName { get; set; }


        public ICollection<TestResultDetailDto> TestResults { get; set; } = new List<TestResultDetailDto>();
    }
}
