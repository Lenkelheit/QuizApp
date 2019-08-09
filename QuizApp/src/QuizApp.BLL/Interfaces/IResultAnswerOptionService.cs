using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.DTO.ResultAnswerOption;

namespace QuizApp.BLL.Interfaces
{
    public interface IResultAnswerOptionService
    {
        Task<ResultAnswerOptionDetailDTO> GetAnswerOptionById(int answerOptionId);

        Task<CreatedResultAnswerOptionDTO> CreateAnswerOption(NewResultAnswerOptionDTO newAnswerOptionDTO);

        Task<DeletedResultAnswerOptionDTO> DeleteAnswerOption(int answerOptionId);
    }
}
