using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QuizApp.BLL.Dto.PassingTest;
using QuizApp.BLL.Dto.Test;
using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.BLL.Dto.Url;

namespace QuizApp.BLL.Interfaces
{
    public interface IPassingTestService
    {
        Task<UrlValidationResultDto> CheckIsUrlValid(int urlId);

        Task<UserIdentificationResultDto> IdentifyUser(IdentityUrlDto urlDto);
    }
}
