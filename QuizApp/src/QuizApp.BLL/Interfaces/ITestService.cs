using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.Test;
using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.BLL.Dto.TestResult;
using QuizApp.BLL.Dto.Url;

namespace QuizApp.BLL.Interfaces
{
    public interface ITestService
    {
        TestsApiDto GetTests(int page, int amountTestsPerPage);

        Task<TestDetailDto> GetTestById(int testId);

        Task<CreatedTestDto> CreateTest(NewTestDto newTestDto);

        Task<UpdatedTestDto> UpdateTest(UpdateTestDto updateTestDto);

        Task<DeletedTestDto> DeleteTest(int testId);

        IEnumerable<TestQuestionDto> GetQuestionsByTestId(int testId);

        UrlsApiDto GetUrlsByTestId(int testId, int page, int amountUrlsPerPage);

        TestResultsApiDto GetResultsByTestId(int testId, int page, int amountResultsPerPage);

        Task<ViewTestDto> GetPassingTestById(int testId);
    }
}
