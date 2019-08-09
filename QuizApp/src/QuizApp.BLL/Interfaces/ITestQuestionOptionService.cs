using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.DTO.ResultAnswerOption;
using QuizApp.BLL.DTO.TestQuestionOption;

namespace QuizApp.BLL.Interfaces
{
    public interface ITestQuestionOptionService
    {
        Task<TestQuestionOptionDetailDTO> GetQuestionOptionById(int questionOptionId);

        Task<CreatedTestQuestionOptionDTO> CreateQuestionOption(NewTestQuestionOptionDTO newQuestionOptionDTO);

        Task<UpdatedTestQuestionOptionDTO> UpdateQuestionOption(UpdatedTestQuestionOptionDTO updatedQuestionOptionDTO);

        Task<DeletedTestQuestionOptionDTO> DeleteQuestionOption(int questionOptionId);

        IEnumerable<ResultAnswerOptionFromQuestionOptionDTO> GetAnswerOptionsByQuestionOptionId(int questionOptionId);
    }
}
