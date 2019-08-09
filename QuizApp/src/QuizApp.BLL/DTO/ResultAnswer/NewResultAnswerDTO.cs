using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.DTO.ResultAnswer
{
    public class NewResultAnswerDTO
    {
        public TimeSpan TimeTakenSeconds { get; set; }

        public int? QuestionId { get; set; }

        public int ResultId { get; set; }
    }
}
