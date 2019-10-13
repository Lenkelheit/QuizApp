using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.TestResult;
using QuizApp.BLL.Dto.Url;

namespace QuizApp.BLL.Interfaces
{
    public interface IUrlService
    {
        IEnumerable<UrlDto> GetUrls();

        Task<UrlDetailDto> GetUrlById(int urlId);

        Task<CreatedUrlDto> CreateUrl(NewUrlDto newUrlDto);

        Task<UpdatedUrlDto> UpdateUrl(UpdateUrlDto updateUrlDto);

        IEnumerable<TestResultDto> GetTestResultsByUrlId(int urlId);
    }
}
