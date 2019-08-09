using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.DTO.ResultAnswer;

namespace QuizApp.BLL.MappingProfiles
{
    public class ResultAnswerProfile : Profile
    {
        public ResultAnswerProfile()
        {
            CreateMap<ResultAnswer, ResultAnswerFromQuestionDTO>();
            CreateMap<ResultAnswer, ResultAnswerFromResultDTO>();
            CreateMap<ResultAnswer, ResultAnswerDetailDTO>();
            CreateMap<NewResultAnswerDTO, ResultAnswer>();
            CreateMap<ResultAnswer, CreatedResultAnswerDTO>();
            CreateMap<ResultAnswer, DeletedResultAnswerDTO>();
        }
    }
}
