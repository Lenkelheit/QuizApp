using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.DTO.ResultAnswer;
using QuizApp.BLL.DTO.TestQuestion;
using QuizApp.BLL.DTO.TestQuestionOption;

namespace QuizApp.BLL.Interfaces
{
    public interface ITestQuestionService
    {
        Task<TestQuestionDetailDTO> GetQuestionById(int questionId);

        Task<CreatedTestQuestionDTO> CreateQuestion(NewTestQuestionDTO newTestQuestionDTO);

        Task<UpdatedTestQuestionDTO> UpdateQuestion(UpdatedTestQuestionDTO updatedTestQuestionDTO);

        Task<DeletedTestQuestionDTO> DeleteQuestion(int questionId);

        IEnumerable<TestQuestionOptionDTO> GetQuestionOptionsByQuestionId(int questionId);

        IEnumerable<ResultAnswerFromQuestionDTO> GetResultAnswersByQuestionId(int questionId);
    }
}
