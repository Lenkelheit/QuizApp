using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.Dto.Test;
using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.BLL.Dto.Url;

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
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.testRepository = unitOfWork.GetRepository<Test, ITestRepository>() ?? throw new NullReferenceException(nameof(testRepository));
            this.testQuestionRepository = unitOfWork.GetRepository<TestQuestion, ITestQuestionRepository>() ?? throw new NullReferenceException(nameof(testQuestionRepository));
            this.urlRepository = unitOfWork.GetRepository<Url, IUrlRepository>() ?? throw new NullReferenceException(nameof(urlRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public IEnumerable<TestDto> GetTests()
        {
            IEnumerable<Test> tests = testRepository.Get();

            return mapper.Map<IEnumerable<TestDto>>(tests);
        }

        public async Task<TestDetailDto> GetTestById(int testId)
        {
            Test test = await testRepository.GetByIdAsync(id: testId, includeProperties: prop => prop
                .Include(t => t.Urls)
                .Include(t => t.TestQuestions)
                    .ThenInclude(q => q.TestQuestionOptions));

            return mapper.Map<TestDetailDto>(test);
        }

        public async Task<CreatedTestDto> CreateTest(NewTestDto newTestDto)
        {
            Test test = mapper.Map<Test>(newTestDto);

            test.LastModifiedDate = DateTime.Now;
            testRepository.Insert(test);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestDto>(test);
        }

        public async Task<UpdatedTestDto> UpdateTest(UpdateTestDto updateTestDto)
        {
            Test test = await testRepository.GetByIdAsync(updateTestDto.Id);
            if (test == null)
            {
                return null;
            }

            test = mapper.Map<Test>(updateTestDto);

            test.LastModifiedDate = DateTime.Now;
            testRepository.Update(test);
            await unitOfWork.SaveAsync();

            return mapper.Map<UpdatedTestDto>(test);
        }

        public async Task<DeletedTestDto> DeleteTest(int testId)
        {
            Test test = await testRepository.GetByIdAsync(testId);
            if (test == null)
            {
                return null;
            }

            testRepository.Delete(test);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedTestDto>(test);
        }

        public IEnumerable<TestQuestionDto> GetQuestionsByTestId(int testId)
        {
            IEnumerable<TestQuestion> testQuestions = testQuestionRepository.Get(filter: q => q.TestId == testId);

            return mapper.Map<IEnumerable<TestQuestionDto>>(testQuestions);
        }

        public IEnumerable<UrlDto> GetUrlsByTestId(int testId)
        {
            IEnumerable<Url> urls = urlRepository.Get(filter: u => u.TestId == testId);

            return mapper.Map<IEnumerable<UrlDto>>(urls);
        }
    }
}
