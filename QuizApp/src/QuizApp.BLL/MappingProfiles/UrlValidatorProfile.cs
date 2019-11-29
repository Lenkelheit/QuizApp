using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation.Results;

using QuizApp.BLL.Dto.UrlValidator;

namespace QuizApp.BLL.MappingProfiles
{
    public class UrlValidatorProfile : Profile
    {
        public UrlValidatorProfile()
        {
            CreateMap<ValidationResult, UrlValidationResultDto>().ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors.Select(e => e.ErrorMessage).ToList()));
            CreateMap<ValidationResult, UserIdentificationResultDto>().ForMember(dest => dest.IsIdentified, opt => opt.MapFrom(src => src.IsValid))
                .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors.Select(e => e.ErrorMessage).ToList()));
        }
    }
}
