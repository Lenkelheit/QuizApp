using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;

using QuizApp.BLL.Dto.Authentication;
using QuizApp.Entities;

namespace QuizApp.BLL.MappingProfiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<User, UserLoggedinDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserRegisteredDto>();
        }
    }
}
