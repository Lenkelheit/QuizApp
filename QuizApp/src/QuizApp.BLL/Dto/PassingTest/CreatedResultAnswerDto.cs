using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.PassingTest
{
    public class CreatedResultAnswerDto
    {
        public int Id { get; set; }

        public TimeSpan TimeTakenSeconds { get; set; }


        public ICollection<CreatedResultAnswerOptionDto> ResultAnswerOptions { get; set; } = new List<CreatedResultAnswerOptionDto>();
    }
}
