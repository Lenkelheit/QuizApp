using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.DTO.TestResult;
using QuizApp.BLL.DTO.Url;
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
            this.unitOfWork = unitOfWork ?? throw new NullReferenceException("UnitOfWork is null.");
            this.urlRepository = unitOfWork.GetRepository<Url, IUrlRepository>();
            this.testResultRepository = unitOfWork.GetRepository<TestResult, ITestResultRepository>();
            this.mapper = mapper;
        }


        public async Task<UrlDetailDTO> GetUrlById(int urlId)
        {
            Url url = await urlRepository.GetByIdAsync(id: urlId, includeProperties: prop => prop
                .Include(u => u.TestResults));

            return mapper.Map<UrlDetailDTO>(url);
        }

        public async Task<CreatedUrlDTO> CreateUrl(NewUrlDTO newUrlDTO)
        {
            Url url = mapper.Map<Url>(newUrlDTO);

            urlRepository.Insert(url);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedUrlDTO>(url);
        }

        public async Task<UpdatedUrlDTO> UpdateUrl(UpdatedUrlDTO updatedUrlDTO)
        {
            Url url = await urlRepository.GetByIdAsync(updatedUrlDTO.Id);
            if (url == null)
            {
                return null;
            }
            url = mapper.Map<Url>(updatedUrlDTO);

            urlRepository.Update(url);
            await unitOfWork.SaveAsync();

            return updatedUrlDTO;
        }

        public IEnumerable<TestResultDTO> GetTestResultsByUrlId(int urlId)
        {
            IEnumerable<TestResult> testResults = testResultRepository.Get(filter: r => r.UrlId == urlId);

            return mapper.Map<IEnumerable<TestResultDTO>>(testResults);
        }
    }
}
