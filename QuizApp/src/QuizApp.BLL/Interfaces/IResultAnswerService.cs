using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.ResultAnswerOption;

namespace QuizApp.BLL.Interfaces
{
    public interface IResultAnswerService
    {
        Task<ResultAnswerDetailDto> GetResultAnswerById(int resultAnswerId);

        Task<CreatedResultAnswerDto> CreateResultAnswer(NewResultAnswerDto newResultAnswerDto);

        Task<DeletedResultAnswerDto> DeleteResultAnswer(int resultAnswerId);

        IEnumerable<ResultAnswerOptionDto> GetAnswerOptionsByResultAnswerId(int resultAnswerId);
    }
}
