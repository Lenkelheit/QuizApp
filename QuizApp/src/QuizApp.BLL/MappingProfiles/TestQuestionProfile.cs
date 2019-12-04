using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.Dto.TestQuestion;

namespace QuizApp.BLL.MappingProfiles
{
    public class TestQuestionProfile : Profile
    {
        public TestQuestionProfile()
        {
            CreateMap<TestQuestion, TestQuestionDto>();
            CreateMap<TestQuestion, TestQuestionDetailDto>();
            CreateMap<NewTestQuestionDto, TestQuestion>();
            CreateMap<TestQuestion, CreatedTestQuestionDto>();
            CreateMap<UpdateTestQuestionDto, TestQuestion>();
            CreateMap<TestQuestion, UpdatedTestQuestionDto>();
            CreateMap<TestQuestion, DeletedTestQuestionDto>();
            CreateMap<TestQuestion, ResultQuestionDto>();
            CreateMap<TestQuestion, ViewQuestionDto>();
        }
    }
}
