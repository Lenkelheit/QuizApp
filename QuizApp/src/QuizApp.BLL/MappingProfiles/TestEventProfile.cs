using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.Dto.TestEvent;

namespace QuizApp.BLL.MappingProfiles
{
    public class TestEventProfile : Profile
    {
        public TestEventProfile()
        {
            CreateMap<NewTestEventDto, TestEvent>();
            CreateMap<TestEvent, CreatedTestEventDto>();
        }
    }
}
