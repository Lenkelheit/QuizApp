﻿using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class ResultAnswer
    {
        public int Id { get; set; }
        public TimeSpan TakeTimeSeconds { get; set; }

        public TestResult Result { get; set; }
        public int? QuestionId { get; set; }
        public TestQuestion Question { get; set; }
        public ICollection<ResultAnswerOption> ResultAnswerOptions { get; set; } = new List<ResultAnswerOption>();
    }
}
