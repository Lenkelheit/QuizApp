using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using QuizApp.Entities;
using QuizApp.BLL.Dto.Url;

namespace QuizApp.BLL.MappingProfiles
{
    public class UrlProfile : Profile
    {
        public UrlProfile()
        {
            CreateMap<Url, UrlDto>();
            CreateMap<Url, UrlDetailDto>();
            CreateMap<NewUrlDto, Url>();
            CreateMap<Url, CreatedUrlDto>();
            CreateMap<UpdateUrlDto, Url>();
            CreateMap<Url, UpdatedUrlDto>();
        }
    }
}
