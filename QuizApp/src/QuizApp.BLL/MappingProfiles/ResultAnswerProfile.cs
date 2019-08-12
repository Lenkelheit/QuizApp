using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.Dto.ResultAnswer;

namespace QuizApp.BLL.MappingProfiles
{
    public class ResultAnswerProfile : Profile
    {
        public ResultAnswerProfile()
        {
            CreateMap<ResultAnswer, ResultAnswerFromQuestionDto>();
            CreateMap<ResultAnswer, ResultAnswerFromResultDto>();
            CreateMap<ResultAnswer, ResultAnswerDetailDto>();
            CreateMap<NewResultAnswerDto, ResultAnswer>();
            CreateMap<ResultAnswer, CreatedResultAnswerDto>();
            CreateMap<ResultAnswer, DeletedResultAnswerDto>();
        }
    }
}
