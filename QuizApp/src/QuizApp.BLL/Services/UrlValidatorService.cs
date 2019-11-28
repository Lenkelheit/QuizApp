using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.Dto.UrlValidator;
using QuizApp.BLL.Validators.UrlValidator;

namespace QuizApp.BLL.Services
{
    public class UrlValidatorService : IUrlValidatorService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IUrlRepository urlRepository;

        private readonly IMapper mapper;


        public UrlValidatorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.urlRepository = unitOfWork.GetRepository<Url, IUrlRepository>() ?? throw new NullReferenceException(nameof(urlRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<UrlValidationResultDto> CheckIsUrlValid(int urlId)
        {
            var url = await urlRepository.GetByIdAsync(id: urlId);

            var urlValidator = new UrlValidator();
            var result = urlValidator.Validate(url);

            return mapper.Map<UrlValidationResultDto>(result);
        }

        public async Task<UserIdentificationResultDto> IdentifyUser(IdentityUrlDto urlDto)
        {
            var url = await urlRepository.GetByIdAsync(id: urlDto.Id);

            var urlValidator = new UrlValidator();
            var urlValidationResult = urlValidator.Validate(url);

            UserIdentificationResultDto userIdentificationResult;

            if (!urlValidationResult.IsValid)
            {
                userIdentificationResult = mapper.Map<UserIdentificationResultDto>(urlValidationResult);
                userIdentificationResult.IsUrlValid = false;
                return userIdentificationResult;
            }

            if (url.NumberOfRuns.HasValue && url.NumberOfRuns > 0)
            {
                --url.NumberOfRuns;
                urlRepository.Update(url);
                await unitOfWork.SaveAsync();
            }

            var intervieweeNameValidator = new UrlIntervieweeNameValidator(urlDto.IntervieweeName);
            var intervieweeNameValidationResult = intervieweeNameValidator.Validate(url);

            userIdentificationResult = mapper.Map<UserIdentificationResultDto>(intervieweeNameValidationResult);
            userIdentificationResult.IsUrlValid = true;
            return userIdentificationResult;
        }
    }
}
