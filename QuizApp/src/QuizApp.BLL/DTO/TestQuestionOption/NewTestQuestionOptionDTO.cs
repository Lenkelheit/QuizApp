using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.DTO.TestQuestionOption
{
    public class NewTestQuestionOptionDTO
    {
        public string Text { get; set; }

        public bool IsRight { get; set; }

        public int QuestionId { get; set; }
    }
}
