using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.Authentication
{
    public class UserAuthenticationResultDto
    {
        public UserLoggedinDto UserLoggedin { get; set; }

        public bool IsValid { get; set; }


        public ICollection<string> Errors { get; set; } = new List<string>();
    }
}
