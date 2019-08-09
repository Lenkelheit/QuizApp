using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.DTO.ResultAnswer;
using QuizApp.BLL.DTO.TestResult;

namespace QuizApp.BLL.Interfaces
{
    public interface ITestResultService
    {
        Task<TestResultDetailDTO> GetTestResultById(int testResultId);

        Task<CreatedTestResultDTO> CreateTestResult(NewTestResultDTO newTestResultDTO);

        Task<DeletedTestResultDTO> DeleteTestResult(int testResultId);

        IEnumerable<ResultAnswerFromResultDTO> GetAnswersByResultId(int testResultId);
    }
}
