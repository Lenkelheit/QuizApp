using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QuizApp.BLL.Dto.PassingTest;

namespace QuizApp.BLL.Interfaces
{
    public interface IPassingTestService
    {
        Task<UrlValidationResultDto> CheckIsUrlValid(int urlId);

        Task<UserIdentificationResultDto> IdentifyUser(IdentityUrlDto urlDto);

        Task<ViewTestDto> GetTestById(int testId);

        Task<CreatedTestResultDto> CreateTestResult(UserUrlDto userUrlDto);
    }
}
