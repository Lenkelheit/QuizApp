using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using QuizApp.BLL.Dto.PassingTest;
using QuizApp.Entities;

namespace QuizApp.BLL.MappingProfiles
{
    public class PassingTestProfile : Profile
    {
        public PassingTestProfile()
        {
            CreateMap<TestResult, CreatedTestResultDto>();
            CreateMap<ResultAnswer, CreatedResultAnswerDto>();
            CreateMap<ResultAnswerOption, CreatedResultAnswerOptionDto>();
        }
    }
}
