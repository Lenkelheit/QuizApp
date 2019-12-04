using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.Dto.TestResult;

namespace QuizApp.BLL.MappingProfiles
{
    public class TestResultProfile : Profile
    {
        public TestResultProfile()
        {
            CreateMap<TestResult, TestResultDto>();
            CreateMap<NewTestResultDto, TestResult>();
            CreateMap<TestResult, CreatedTestResultDto>();
            CreateMap<TestResult, DeletedTestResultDto>();
            CreateMap<TestResult, TestResultDetailDto>().ForMember(dest => dest.Test, opt => opt.MapFrom(src => src.Url.Test));
        }
    }
}
