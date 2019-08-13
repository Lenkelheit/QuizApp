using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.BLL.Dto.TestQuestionOption;

namespace QuizApp.BLL.Interfaces
{
    public interface ITestQuestionService
    {
        Task<TestQuestionDetailDto> GetQuestionById(int questionId);

        Task<CreatedTestQuestionDto> CreateQuestion(NewTestQuestionDto newTestQuestionDto);

        Task<UpdatedTestQuestionDto> UpdateQuestion(UpdateTestQuestionDto updateTestQuestionDto);

        Task<DeletedTestQuestionDto> DeleteQuestion(int questionId);

        IEnumerable<TestQuestionOptionDto> GetQuestionOptionsByQuestionId(int questionId);

        IEnumerable<ResultAnswerFromQuestionDto> GetResultAnswersByQuestionId(int questionId);
    }
}
