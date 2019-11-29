using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Dto.TestResult;
using QuizApp.BLL.Dto.Url;
using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.Dto.Test;

namespace QuizApp.BLL.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ITestRepository testRepository;

        private readonly IUrlRepository urlRepository;

        private readonly ITestResultRepository testResultRepository;

        private readonly IMapper mapper;


        public UrlService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.testRepository = unitOfWork.GetRepository<Test, ITestRepository>() ?? throw new NullReferenceException(nameof(testRepository));
            this.urlRepository = unitOfWork.GetRepository<Url, IUrlRepository>() ?? throw new NullReferenceException(nameof(urlRepository));
            this.testResultRepository = unitOfWork.GetRepository<TestResult, ITestResultRepository>() ?? throw new NullReferenceException(nameof(testResultRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public UrlsApiDto GetUrls(int page, int amountUrlsPerPage)
        {
            var urls = urlRepository.GetPageWithAmount(page: page, amountPerPage: amountUrlsPerPage);

            return new UrlsApiDto
            {
                Urls = mapper.Map<List<UrlDto>>(urls),
                TotalCount = urlRepository.Count()
            };
        }

        public async Task<UrlDetailDto> GetUrlById(int urlId)
        {
            Url url = await urlRepository.GetByIdAsync(id: urlId, includeProperties: prop => prop
                .Include(u => u.TestResults)
                .Include(u => u.Test));

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

        public TestPreviewDto GetTestByUrlId(int urlId)
        {
            Test testByUrlId = testRepository.Get(filter: test => test.Urls.Any(url => url.Id == urlId)).FirstOrDefault();

            return mapper.Map<TestPreviewDto>(testByUrlId);
        }

        public TestResultsApiDto GetTestResultsByUrlId(int urlId, int page, int amountResultsPerPage)
        {
            var testResults = testResultRepository.GetPageWithAmount(filter: r => r.UrlId == urlId, page: page, amountPerPage: amountResultsPerPage);

            return new TestResultsApiDto
            {
                TestResults = mapper.Map<List<TestResultDto>>(testResults),
                TotalCount = testResultRepository.Count(predicate: r => r.UrlId == urlId)
            };
        }
    }
}
