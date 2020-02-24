using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.Authentication
{
    public class UserRegisterDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
