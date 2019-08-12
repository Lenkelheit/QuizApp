using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.TestQuestionOption;

namespace QuizApp.BLL.Dto.TestQuestion
{
    public class TestQuestionDetailDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Hint { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }


        public ICollection<TestQuestionOptionDetailDto> TestQuestionOptions { get; set; } = new List<TestQuestionOptionDetailDto>();

        public ICollection<ResultAnswerDetailDto> ResultAnswers { get; set; } = new List<ResultAnswerDetailDto>();
    }
}
