using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.DTO.TestQuestion;

namespace QuizApp.BLL.MappingProfiles
{
    public class TestQuestionProfile : Profile
    {
        public TestQuestionProfile()
        {
            CreateMap<TestQuestion, TestQuestionDTO>();
            CreateMap<TestQuestion, TestQuestionDetailDTO>();
            CreateMap<NewTestQuestionDTO, TestQuestion>();
            CreateMap<TestQuestion, CreatedTestQuestionDTO>();
            CreateMap<UpdatedTestQuestionDTO, TestQuestion>();
            CreateMap<TestQuestion, DeletedTestQuestionDTO>();
        }
    }
}
