using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.TestResult;

namespace QuizApp.BLL.Interfaces
{
    public interface ITestResultService
    {
        Task<TestResultDetailDto> GetTestResultById(int testResultId);

        Task<CreatedTestResultDto> CreateTestResult(NewTestResultDto newTestResultDto);

        Task<DeletedTestResultDto> DeleteTestResult(int testResultId);

        IEnumerable<ResultAnswerFromResultDto> GetAnswersByResultId(int testResultId);
    }
}
