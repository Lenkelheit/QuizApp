using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QuizApp.BLL.Dto.Authentication;

namespace QuizApp.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        UserAuthenticationResultDto AuthenticateUser(UserLoginDto userLoginDto);
    }
}
