using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.ResultAnswer
{
    public class NewResultAnswerDto
    {
        public TimeSpan TimeTakenSeconds { get; set; }

        public int? QuestionId { get; set; }

        public int ResultId { get; set; }
    }
}
