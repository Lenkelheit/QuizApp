using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.Dto.ResultAnswerOption;

namespace QuizApp.BLL.MappingProfiles
{
    public class ResultAnswerOptionProfile : Profile
    {
        public ResultAnswerOptionProfile()
        {
            CreateMap<ResultAnswerOption, ResultAnswerOptionDto>();
            CreateMap<ResultAnswerOption, ResultAnswerOptionDetailDto>();
            CreateMap<NewResultAnswerOptionDto, ResultAnswerOption>();
            CreateMap<ResultAnswerOption, CreatedResultAnswerOptionDto>();
            CreateMap<ResultAnswerOption, DeletedResultAnswerOptionDto>();
            CreateMap<ResultAnswerOption, ResultAnswerOptionFromQuestionOptionDto>();
        }
    }
}
