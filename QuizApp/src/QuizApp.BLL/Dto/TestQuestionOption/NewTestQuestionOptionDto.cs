using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.TestQuestionOption
{
    public class NewTestQuestionOptionDto
    {
        public string Text { get; set; }

        public bool IsRight { get; set; }

        public int QuestionId { get; set; }
    }
}
