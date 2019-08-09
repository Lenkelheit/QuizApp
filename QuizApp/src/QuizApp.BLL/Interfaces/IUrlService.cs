using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.DTO.TestResult;
using QuizApp.BLL.DTO.Url;

namespace QuizApp.BLL.Interfaces
{
    public interface IUrlService
    {
        Task<UrlDetailDTO> GetUrlById(int urlId);

        Task<CreatedUrlDTO> CreateUrl(NewUrlDTO newUrlDTO);

        Task<UpdatedUrlDTO> UpdateUrl(UpdatedUrlDTO updatedUrlDTO);

        IEnumerable<TestResultDTO> GetTestResultsByUrlId(int urlId);
    }
}
