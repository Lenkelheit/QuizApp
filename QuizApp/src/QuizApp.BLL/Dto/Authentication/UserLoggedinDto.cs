﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.Authentication
{
    public class UserLoggedinDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
