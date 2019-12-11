using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using FluentValidation.Results;

using QuizApp.BLL.Settings;
using QuizApp.BLL.Dto.Authentication;

namespace QuizApp.BLL.MappingProfiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<UserLogin, UserLoggedinDto>();
            CreateMap<ValidationResult, UserAuthenticationResultDto>().ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors.Select(e => e.ErrorMessage).ToList()));
        }
    }
}
