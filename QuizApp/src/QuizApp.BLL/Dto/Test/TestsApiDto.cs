using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.Test
{
    public class TestsApiDto
    {
        public int TotalCount { get; set; }


        public ICollection<TestDto> Tests { get; set; } = new List<TestDto>();
    }
}
