using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.Dto.PassingTest;
using QuizApp.BLL.Validators.PassingTest;
using FluentValidation.Results;

namespace QuizApp.BLL.Services
{
    public class PassingTestService : IPassingTestService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ITestRepository testRepository;

        private readonly ITestQuestionRepository testQuestionRepository;

        private readonly IUrlRepository urlRepository;

        private readonly IMapper mapper;


        public PassingTestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.testRepository = unitOfWork.GetRepository<Test, ITestRepository>() ?? throw new NullReferenceException(nameof(testRepository));
            this.testQuestionRepository = unitOfWork.GetRepository<TestQuestion, ITestQuestionRepository>() ?? throw new NullReferenceException(nameof(testQuestionRepository));
            this.urlRepository = unitOfWork.GetRepository<Url, IUrlRepository>() ?? throw new NullReferenceException(nameof(urlRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<UrlValidationResultDto> CheckIsUrlValid(int urlId)
        {
            Url url = await urlRepository.GetByIdAsync(id: urlId);

            UrlValidator urlValidator = new UrlValidator();
            ValidationResult result = urlValidator.Validate(url);

            return mapper.Map<UrlValidationResultDto>(result);
        }

        public async Task<UserIdentificationResultDto> IdentifyUser(IdentityUrlDto urlDto)
        {
            Url url = await urlRepository.GetByIdAsync(id: urlDto.Id);

            UrlValidator urlValidator = new UrlValidator();
            ValidationResult urlValidationResult = urlValidator.Validate(url);

            UserIdentificationResultDto userIdentificationResult;

            if (!urlValidationResult.IsValid) 
            {
                userIdentificationResult = mapper.Map<UserIdentificationResultDto>(urlValidationResult);
                userIdentificationResult.IsUrlValid = false;
                return userIdentificationResult;
            }

            if (url.NumberOfRuns != null && url.NumberOfRuns > 0)
            {
                --url.NumberOfRuns;
                urlRepository.Update(url);
                await unitOfWork.SaveAsync();
            }

            UrlIntervieweeNameValidator intervieweeNameValidator = new UrlIntervieweeNameValidator(urlDto.IntervieweeName);
            ValidationResult intervieweeNameValidationResult = intervieweeNameValidator.Validate(url);

            userIdentificationResult = mapper.Map<UserIdentificationResultDto>(intervieweeNameValidationResult);
            userIdentificationResult.IsUrlValid = true;
            return userIdentificationResult;
        }
    }
}
