﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    public class ResultAnswer
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan TimeTakenSeconds { get; set; }

        public int? QuestionId { get; set; }

        public int ResultId { get; set; }


        [ForeignKey(nameof(QuestionId))]
        public TestQuestion Question { get; set; }

        [ForeignKey(nameof(ResultId))]
        public TestResult Result { get; set; }

        public ICollection<ResultAnswerOption> ResultAnswerOptions { get; set; } = new List<ResultAnswerOption>();
    }
}
