﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.DTO.TestResult
{
    public class CreatedTestResultDTO
    {
        public int Id { get; set; }

        public string IntervieweeName { get; set; }

        public DateTime PassingStartTime { get; set; }

        public DateTime PassingEndTime { get; set; }

        public int Score { get; set; }
    }
}
