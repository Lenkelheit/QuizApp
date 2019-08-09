using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.DTO.TestResult;

namespace QuizApp.BLL.MappingProfiles
{
    public class TestResultProfile : Profile
    {
        public TestResultProfile()
        {
            CreateMap<TestResult, TestResultDTO>();
            CreateMap<TestResult, TestResultDetailDTO>();
            CreateMap<NewTestResultDTO, TestResult>();
            CreateMap<TestResult, CreatedTestResultDTO>();
            CreateMap<TestResult, DeletedTestResultDTO>();
        }
    }
}
