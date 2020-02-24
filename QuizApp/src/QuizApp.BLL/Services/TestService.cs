using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;

using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.Dto.Test;
using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.BLL.Dto.Url;
using QuizApp.BLL.Dto.TestResult;

namespace QuizApp.BLL.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IUserRepository userRepository;

        private readonly ITestRepository testRepository;

        private readonly ITestQuestionRepository testQuestionRepository;

        private readonly IUrlRepository urlRepository;

        private readonly ITestResultRepository testResultRepository;

        private readonly IMapper mapper;


        public TestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.userRepository = unitOfWork.GetRepository<User, IUserRepository>() ?? throw new NullReferenceException(nameof(userRepository));
            this.testRepository = unitOfWork.GetRepository<Test, ITestRepository>() ?? throw new NullReferenceException(nameof(testRepository));
            this.testQuestionRepository = unitOfWork.GetRepository<TestQuestion, ITestQuestionRepository>() ?? throw new NullReferenceException(nameof(testQuestionRepository));
            this.urlRepository = unitOfWork.GetRepository<Url, IUrlRepository>() ?? throw new NullReferenceException(nameof(urlRepository));
            this.testResultRepository = unitOfWork.GetRepository<TestResult, ITestResultRepository>() ?? throw new NullReferenceException(nameof(testResultRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public TestsApiDto GetTests(int page, int amountTestsPerPage, string userEmail)
        {
            var tests = testRepository.GetPageWithAmount(filter: t => t.Author.Email == userEmail, page: page, amountPerPage: amountTestsPerPage);

            return new TestsApiDto
            {
                Tests = mapper.Map<List<TestDto>>(tests),
                TotalCount = testRepository.Count(t => t.Author.Email == userEmail)
            };
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

        public UrlsApiDto GetUrlsByTestId(int testId, int page, int amountUrlsPerPage)
        {
            var urls = urlRepository.GetPageWithAmount(filter: u => u.TestId == testId, page: page, amountPerPage: amountUrlsPerPage);

            return new UrlsApiDto
            {
                Urls = mapper.Map<List<UrlDto>>(urls),
                TotalCount = urlRepository.Count(predicate: u => u.TestId == testId)
            };
        }

        public TestResultsApiDto GetResultsByTestId(int testId, int page, int amountResultsPerPage)
        {
            var testResults = testResultRepository.GetPageWithAmount(filter: r => r.Url.TestId == testId, page: page, amountPerPage: amountResultsPerPage);

            return new TestResultsApiDto
            {
                TestResults = mapper.Map<List<TestResultDto>>(testResults),
                TotalCount = testResultRepository.Count(predicate: r => r.Url.TestId == testId)
            };
        }

        public async Task<ViewTestDto> GetPassingTestById(int testId)
        {
            var test = await testRepository.GetByIdAsync(id: testId, includeProperties: prop => prop
                .Include(t => t.TestQuestions)
                    .ThenInclude(q => q.TestQuestionOptions));

            return mapper.Map<ViewTestDto>(test);
        }
    }
}
