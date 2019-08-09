using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.DTO.TestQuestionOption;

namespace QuizApp.BLL.MappingProfiles
{
    public class TestQuestionOptionProfile : Profile
    {
        public TestQuestionOptionProfile()
        {
            CreateMap<TestQuestionOption, TestQuestionOptionDTO>();
            CreateMap<TestQuestionOption, TestQuestionOptionDetailDTO>();
            CreateMap<NewTestQuestionOptionDTO, TestQuestionOption>();
            CreateMap<TestQuestionOption, CreatedTestQuestionOptionDTO>();
            CreateMap<UpdatedTestQuestionOptionDTO, TestQuestionOption>();
            CreateMap<TestQuestionOption, DeletedTestQuestionOptionDTO>();
        }
    }
}
