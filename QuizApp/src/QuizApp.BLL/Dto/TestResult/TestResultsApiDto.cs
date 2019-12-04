using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.TestResult
{
    public class TestResultsApiDto
    {
        public int TotalCount { get; set; }


        public ICollection<TestResultDto> TestResults { get; set; } = new List<TestResultDto>();
    }
}
