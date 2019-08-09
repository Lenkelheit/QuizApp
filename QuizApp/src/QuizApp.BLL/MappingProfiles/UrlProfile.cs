using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.DTO.Url;

namespace QuizApp.BLL.MappingProfiles
{
    public class UrlProfile : Profile
    {
        public UrlProfile()
        {
            CreateMap<Url, UrlDTO>();
            CreateMap<Url, UrlDetailDTO>();
            CreateMap<NewUrlDTO, Url>();
            CreateMap<Url, CreatedUrlDTO>();
            CreateMap<UpdatedUrlDTO, Url>();
        }
    }
}
