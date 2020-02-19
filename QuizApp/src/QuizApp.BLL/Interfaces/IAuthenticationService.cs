using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QuizApp.BLL.Dto.Authentication;

namespace QuizApp.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        UserLoggedinDto GetUser(UserLoginDto userLoginDto);

        UserLoggedinDto GetUserByEmail(string userEmail);
    }
}
