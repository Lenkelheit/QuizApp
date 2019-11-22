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
        IEnumerable<TestResultDto> GetTestResults(string intervieweeNameFilter);

        Task<TestResultDetailDto> GetTestResultById(int testResultId);

        Task<CreatedTestResultDto> CreateTestResult(NewTestResultDto newTestResultDto);

        Task<DeletedTestResultDto> DeleteTestResult(int testResultId);

        ResultAnswersApiDto GetAnswersByResultId(int testResultId, int page, int amountAnswersPerPage);
    }
}
