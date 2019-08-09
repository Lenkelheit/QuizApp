using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.DTO.Test;

namespace QuizApp.BLL.MappingProfiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<NewTestDTO, Test>();
            CreateMap<Test, TestDTO>();
            CreateMap<Test, TestDetailDTO>();
            CreateMap<Test, CreatedTestDTO>();
            CreateMap<UpdatedTestDTO, Test>();
            CreateMap<Test, DeletedTestDTO>();
        }
    }
}
