using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Dto.TestResult;
using QuizApp.BLL.Dto.Url;
using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.BLL.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IUrlRepository urlRepository;

        private readonly ITestResultRepository testResultRepository;

        private readonly IMapper mapper;


        public UrlService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.urlRepository = unitOfWork.GetRepository<Url, IUrlRepository>() ?? throw new NullReferenceException(nameof(urlRepository));
            this.testResultRepository = unitOfWork.GetRepository<TestResult, ITestResultRepository>() ?? throw new NullReferenceException(nameof(testResultRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<UrlDetailDto> GetUrlById(int urlId)
        {
            Url url = await urlRepository.GetByIdAsync(id: urlId, includeProperties: prop => prop
                .Include(u => u.TestResults));

            return mapper.Map<UrlDetailDto>(url);
        }

        public async Task<CreatedUrlDto> CreateUrl(NewUrlDto newUrlDto)
        {
            Url url = mapper.Map<Url>(newUrlDto);

            urlRepository.Insert(url);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedUrlDto>(url);
        }

        public async Task<UpdatedUrlDto> UpdateUrl(UpdateUrlDto updateUrlDto)
        {
            Url url = await urlRepository.GetByIdAsync(updateUrlDto.Id);
            if (url == null)
            {
                return null;
            }

            url = mapper.Map<Url>(updateUrlDto);

            urlRepository.Update(url);
            await unitOfWork.SaveAsync();

            return mapper.Map<UpdatedUrlDto>(url);
        }

        public IEnumerable<TestResultDto> GetTestResultsByUrlId(int urlId)
        {
            IEnumerable<TestResult> testResults = testResultRepository.Get(filter: r => r.UrlId == urlId);

            return mapper.Map<IEnumerable<TestResultDto>>(testResults);
        }
    }
}
