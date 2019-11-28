using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.PassingTest;

namespace QuizApp.BLL.Interfaces
{
    public interface IPassingTestService
    {
        Task<CreatedTestResultDto> CreateTestResult(UserUrlDto userUrlDto);
    }
}
