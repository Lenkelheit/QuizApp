using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using QuizApp.BLL.Dto.UrlValidator;

namespace QuizApp.BLL.Interfaces
{
    public interface IUrlValidatorService
    {
        Task<UrlValidationResultDto> CheckIsUrlValid(int urlId);

        Task<UserIdentificationResultDto> IdentifyUser(IdentityUrlDto urlDto);
    }
}
