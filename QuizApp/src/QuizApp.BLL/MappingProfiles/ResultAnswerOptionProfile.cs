using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.DTO.ResultAnswerOption;

namespace QuizApp.BLL.MappingProfiles
{
    public class ResultAnswerOptionProfile : Profile
    {
        public ResultAnswerOptionProfile()
        {
            CreateMap<ResultAnswerOption, ResultAnswerOptionDTO>();
            CreateMap<ResultAnswerOption, ResultAnswerOptionDetailDTO>();
            CreateMap<NewResultAnswerOptionDTO, ResultAnswerOption>();
            CreateMap<ResultAnswerOption, CreatedResultAnswerOptionDTO>();
            CreateMap<ResultAnswerOption, DeletedResultAnswerOptionDTO>();
            CreateMap<ResultAnswerOption, ResultAnswerOptionFromQuestionOptionDTO>();
        }
    }
}
