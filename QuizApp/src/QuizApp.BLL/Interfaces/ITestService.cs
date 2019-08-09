using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.DTO.Test;
using QuizApp.BLL.DTO.TestQuestion;
using QuizApp.BLL.DTO.Url;

namespace QuizApp.BLL.Interfaces
{
    public interface ITestService
    {
        IEnumerable<TestDTO> GetTests();

        Task<TestDetailDTO> GetTestById(int testId);

        Task<CreatedTestDTO> CreateTest(NewTestDTO newTestDTO);

        Task<UpdatedTestDTO> UpdateTest(UpdatedTestDTO updatedTestDTO);

        Task<DeletedTestDTO> DeleteTest(int testId);

        IEnumerable<TestQuestionDTO> GetQuestionsByTestId(int testId);

        IEnumerable<UrlDTO> GetUrlsByTestId(int testId);
    }
}
