using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;
using FluentValidation.Results;
using QuizApp.BLL.Dto.PassingTest;
using QuizApp.Entities;

namespace QuizApp.BLL.MappingProfiles
{
    public class PassingTestProfile : Profile
    {
        public PassingTestProfile()
        {
            CreateMap<ValidationResult, UrlValidationResultDto>().ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors.Select(e => e.ErrorMessage).ToList()));
            CreateMap<ValidationResult, UserIdentificationResultDto>().ForMember(dest => dest.IsIdentified, opt => opt.MapFrom(src => src.IsValid))
                .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors.Select(e => e.ErrorMessage).ToList()));

            CreateMap<Test, ViewTestDto>();
            CreateMap<TestQuestion, ViewQuestionDto>();
            CreateMap<TestQuestionOption, ViewQuestionOptionDto>();

            CreateMap<TestResult, CreatedTestResultDto>();
            CreateMap<ResultAnswer, CreatedResultAnswerDto>();
            CreateMap<ResultAnswerOption, CreatedResultAnswerOptionDto>();
        }
    }
}
