using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;

using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;
using Microsoft.Extensions.Options;
using QuizApp.BLL.Settings;
using QuizApp.BLL.Dto.Authentication;
using QuizApp.BLL.Validators.AuthenticationValidator;

namespace QuizApp.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IUserRepository userRepository;

        private readonly IMapper mapper;

        private readonly UserLogin userLogin;


        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<UserLogin> userLogin)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.userRepository = unitOfWork.GetRepository<User, IUserRepository>() ?? throw new NullReferenceException(nameof(userRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userLogin = userLogin?.Value ?? throw new ArgumentNullException(nameof(userLogin));
        }


        public UserAuthenticationResultDto AuthenticateUser(UserLoginDto userLoginDto)
        {
            var userLoginValidator = new UserLoginValidator(userLogin);
            var userLoginValidationResult = userLoginValidator.Validate(userLoginDto);

            var userAuthenticationResult = mapper.Map<UserAuthenticationResultDto>(userLoginValidationResult);
            userAuthenticationResult.UserLoggedin = userLoginValidationResult.IsValid ? mapper.Map<UserLoggedinDto>(userLogin) : null;
            return userAuthenticationResult;
        }
    }
}
