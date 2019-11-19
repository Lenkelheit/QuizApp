using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.TestResult;
using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.BLL.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ITestResultRepository testResultRepository;

        private readonly IResultAnswerRepository resultAnswerRepository;

        private readonly IMapper mapper;


        public TestResultService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.testResultRepository = unitOfWork.GetRepository<TestResult, ITestResultRepository>() ?? throw new NullReferenceException(nameof(testResultRepository));
            this.resultAnswerRepository = unitOfWork.GetRepository<ResultAnswer, IResultAnswerRepository>() ?? throw new NullReferenceException(nameof(resultAnswerRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<TestResultDetailDto> GetTestResultById(int testResultId)
        {
            TestResult testResult = await testResultRepository.GetByIdAsync(id: testResultId, includeProperties: prop => prop
                .Include(r => r.ResultAnswers)
                    .ThenInclude(ra => ra.ResultAnswerOptions)
                .Include(r => r.ResultAnswers)
                    .ThenInclude(ra => ra.Question)
                        .ThenInclude(q => q.TestQuestionOptions)
                .Include(r => r.Url)
                    .ThenInclude(u => u.Test));

            return mapper.Map<TestResultDetailDto>(testResult);
        }

        public async Task<CreatedTestResultDto> CreateTestResult(NewTestResultDto newTestResultDto)
        {
            TestResult testResult = mapper.Map<TestResult>(newTestResultDto);

            testResultRepository.Insert(testResult);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestResultDto>(testResult);
        }

        public async Task<DeletedTestResultDto> DeleteTestResult(int testResultId)
        {
            TestResult testResult = await testResultRepository.GetByIdAsync(testResultId);
            if (testResult == null)
            {
                return null;
            }

            testResultRepository.Delete(testResult);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedTestResultDto>(testResult);
        }

        public IEnumerable<ResultAnswerFromResultDto> GetAnswersByResultId(int testResultId)
        {
            IEnumerable<ResultAnswer> resultAnswers = resultAnswerRepository.Get(filter: ra => ra.ResultId == testResultId);

            return mapper.Map<IEnumerable<ResultAnswerFromResultDto>>(resultAnswers);
        }
    }
}
