﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.UrlValidator
{
    public class UrlValidationResultDto
    {
        public bool IsValid { get; set; }


        public ICollection<string> Errors { get; set; } = new List<string>();
    }
}
