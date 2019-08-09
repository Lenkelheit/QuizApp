using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.DTO.Test;
using QuizApp.BLL.DTO.TestQuestion;
using QuizApp.BLL.DTO.Url;

namespace QuizApp.BLL.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ITestRepository testRepository;

        private readonly ITestQuestionRepository testQuestionRepository;

        private readonly IUrlRepository urlRepository;

        private readonly IMapper mapper;


        public TestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new NullReferenceException("UnitOfWork is null.");
            this.testRepository = unitOfWork.GetRepository<Test, ITestRepository>();
            this.testQuestionRepository = unitOfWork.GetRepository<TestQuestion, ITestQuestionRepository>();
            this.urlRepository = unitOfWork.GetRepository<Url, IUrlRepository>();
            this.mapper = mapper;
        }


        public IEnumerable<TestDTO> GetTests()
        {
            IEnumerable<Test> tests = testRepository.Get();

            return mapper.Map<IEnumerable<TestDTO>>(tests);
        }

        public async Task<TestDetailDTO> GetTestById(int testId)
        {
            Test test = await testRepository.GetByIdAsync(id: testId, includeProperties: prop => prop
                .Include(t => t.Urls)
                .Include(t => t.TestQuestions)
                    .ThenInclude(q => q.TestQuestionOptions));

            return mapper.Map<TestDetailDTO>(test);
        }

        public async Task<CreatedTestDTO> CreateTest(NewTestDTO newTestDTO)
        {
            Test test = mapper.Map<Test>(newTestDTO);

            test.LastModifiedDate = DateTime.Now;
            testRepository.Insert(test);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestDTO>(test);
        }

        public async Task<UpdatedTestDTO> UpdateTest(UpdatedTestDTO updatedTestDTO)
        {
            Test test = await testRepository.GetByIdAsync(updatedTestDTO.Id);
            if (test == null)
            {
                return null;
            }
            test = mapper.Map<Test>(updatedTestDTO);

            test.LastModifiedDate = DateTime.Now;
            testRepository.Update(test);
            await unitOfWork.SaveAsync();

            return updatedTestDTO;
        }

        public async Task<DeletedTestDTO> DeleteTest(int testId)
        {
            Test test = await testRepository.GetByIdAsync(testId);
            if (test == null)
            {
                return null;
            }

            testRepository.Delete(test);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedTestDTO>(test);
        }

        public IEnumerable<TestQuestionDTO> GetQuestionsByTestId(int testId)
        {
            IEnumerable<TestQuestion> testQuestions = testQuestionRepository.Get(filter: q => q.TestId == testId);

            return mapper.Map<IEnumerable<TestQuestionDTO>>(testQuestions);
        }

        public IEnumerable<UrlDTO> GetUrlsByTestId(int testId)
        {
            IEnumerable<Url> urls = urlRepository.Get(filter: u => u.TestId == testId);

            return mapper.Map<IEnumerable<UrlDTO>>(urls);
        }
    }
}
