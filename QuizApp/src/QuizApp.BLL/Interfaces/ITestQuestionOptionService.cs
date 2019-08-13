using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.ResultAnswerOption;
using QuizApp.BLL.Dto.TestQuestionOption;

namespace QuizApp.BLL.Interfaces
{
    public interface ITestQuestionOptionService
    {
        Task<TestQuestionOptionDetailDto> GetQuestionOptionById(int questionOptionId);

        Task<CreatedTestQuestionOptionDto> CreateQuestionOption(NewTestQuestionOptionDto newQuestionOptionDto);

        Task<UpdatedTestQuestionOptionDto> UpdateQuestionOption(UpdateTestQuestionOptionDto updateQuestionOptionDto);

        Task<DeletedTestQuestionOptionDto> DeleteQuestionOption(int questionOptionId);

        IEnumerable<ResultAnswerOptionFromQuestionOptionDto> GetAnswerOptionsByQuestionOptionId(int questionOptionId);
    }
}
