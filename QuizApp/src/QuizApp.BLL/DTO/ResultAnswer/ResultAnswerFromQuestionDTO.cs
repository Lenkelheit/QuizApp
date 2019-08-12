using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.ResultAnswer
{
    public class ResultAnswerFromQuestionDto
    {
        public int Id { get; set; }

        public TimeSpan TimeTakenSeconds { get; set; }
    }
}
