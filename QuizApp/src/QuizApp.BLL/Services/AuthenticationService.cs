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
using QuizApp.BLL.Dto.Authentication;

namespace QuizApp.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IUserRepository userRepository;

        private readonly IMapper mapper;


        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.userRepository = unitOfWork.GetRepository<User, IUserRepository>() ?? throw new NullReferenceException(nameof(userRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public UserLoggedinDto GetUser(UserLoginDto userLoginDto)
        {
            var userLogin = userRepository.Get(filter: user => user.Email == userLoginDto.Email && user.Password == userLoginDto.Password).FirstOrDefault();

            return mapper.Map<UserLoggedinDto>(userLogin);
        }

        public UserLoggedinDto GetUserByEmail(string userEmail)
        {
            var userLogin = userEmail != null ? userRepository.Get(filter: user => user.Email == userEmail).FirstOrDefault() : null;

            return mapper.Map<UserLoggedinDto>(userLogin);
        }
    }
}
