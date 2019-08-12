using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.TestQuestionOption
{
    public class DeletedTestQuestionOptionDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsRight { get; set; }
    }
}
