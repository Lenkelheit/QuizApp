using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;

using QuizApp.BLL.Settings;
using QuizApp.BLL.Dto.Authentication;

namespace QuizApp.BLL.MappingProfiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<UserLogin, UserLoggedinDto>();
        }
    }
}
