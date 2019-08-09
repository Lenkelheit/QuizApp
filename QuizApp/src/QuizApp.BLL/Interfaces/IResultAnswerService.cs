using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.DTO.ResultAnswer;
using QuizApp.BLL.DTO.ResultAnswerOption;

namespace QuizApp.BLL.Interfaces
{
    public interface IResultAnswerService
    {
        Task<ResultAnswerDetailDTO> GetResultAnswerById(int resultAnswerId);

        Task<CreatedResultAnswerDTO> CreateResultAnswer(NewResultAnswerDTO newResultAnswerDTO);

        Task<DeletedResultAnswerDTO> DeleteResultAnswer(int resultAnswerId);

        IEnumerable<ResultAnswerOptionDTO> GetAnswerOptionsByResultAnswerId(int resultAnswerId);
    }
}
