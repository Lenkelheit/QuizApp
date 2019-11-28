using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.Dto.TestQuestionOption;

namespace QuizApp.BLL.MappingProfiles
{
    public class TestQuestionOptionProfile : Profile
    {
        public TestQuestionOptionProfile()
        {
            CreateMap<TestQuestionOption, TestQuestionOptionDto>();
            CreateMap<TestQuestionOption, TestQuestionOptionDetailDto>();
            CreateMap<NewTestQuestionOptionDto, TestQuestionOption>();
            CreateMap<TestQuestionOption, CreatedTestQuestionOptionDto>();
            CreateMap<UpdateTestQuestionOptionDto, TestQuestionOption>();
            CreateMap<TestQuestionOption, UpdatedTestQuestionOptionDto>();
            CreateMap<TestQuestionOption, DeletedTestQuestionOptionDto>();
            CreateMap<TestQuestionOption, ResultQuestionOptionDto>();
            CreateMap<TestQuestionOption, ViewQuestionOptionDto>();
        }
    }
}
