using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.Dto.Test;

namespace QuizApp.BLL.MappingProfiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<NewTestDto, Test>();
            CreateMap<Test, TestDto>();
            CreateMap<Test, TestDetailDto>();
            CreateMap<Test, CreatedTestDto>();
            CreateMap<UpdateTestDto, Test>();
            CreateMap<Test, UpdatedTestDto>();
            CreateMap<Test, DeletedTestDto>();
            CreateMap<Test, TestPreviewDto>();
        }
    }
}
