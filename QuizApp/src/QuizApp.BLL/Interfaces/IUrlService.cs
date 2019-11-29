using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QuizApp.BLL.Dto.Test;
using QuizApp.BLL.Dto.TestResult;
using QuizApp.BLL.Dto.Url;

namespace QuizApp.BLL.Interfaces
{
    public interface IUrlService
    {
        UrlsApiDto GetUrls(int page, int amountUrlsPerPage);

        Task<UrlDetailDto> GetUrlById(int urlId);

        Task<CreatedUrlDto> CreateUrl(NewUrlDto newUrlDto);

        Task<UpdatedUrlDto> UpdateUrl(UpdateUrlDto updateUrlDto);

        TestPreviewDto GetTestByUrlId(int urlId);

        TestResultsApiDto GetTestResultsByUrlId(int urlId, int page, int amountResultsPerPage);
    }
}
