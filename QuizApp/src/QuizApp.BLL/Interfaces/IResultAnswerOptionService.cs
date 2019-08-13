using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.ResultAnswerOption;

namespace QuizApp.BLL.Interfaces
{
    public interface IResultAnswerOptionService
    {
        Task<ResultAnswerOptionDetailDto> GetAnswerOptionById(int answerOptionId);

        Task<CreatedResultAnswerOptionDto> CreateAnswerOption(NewResultAnswerOptionDto newAnswerOptionDto);

        Task<DeletedResultAnswerOptionDto> DeleteAnswerOption(int answerOptionId);
    }
}
