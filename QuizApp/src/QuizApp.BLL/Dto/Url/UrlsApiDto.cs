using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.Url
{
    public class UrlsApiDto
    {
        public int TotalCount { get; set; }


        public ICollection<UrlDto> Urls { get; set; } = new List<UrlDto>();
    }
}
