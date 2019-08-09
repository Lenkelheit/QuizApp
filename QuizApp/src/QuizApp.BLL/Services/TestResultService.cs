using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.DTO.ResultAnswer;
using QuizApp.BLL.DTO.TestResult;
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
            this.unitOfWork = unitOfWork ?? throw new NullReferenceException("UnitOfWork is null.");
            this.testResultRepository = unitOfWork.GetRepository<TestResult, ITestResultRepository>();
            this.resultAnswerRepository = unitOfWork.GetRepository<ResultAnswer, IResultAnswerRepository>();
            this.mapper = mapper;
        }


        public async Task<TestResultDetailDTO> GetTestResultById(int testResultId)
        {
            TestResult testResult = await testResultRepository.GetByIdAsync(id: testResultId, includeProperties: prop => prop
                .Include(r => r.ResultAnswers)
                    .ThenInclude(ra => ra.ResultAnswerOptions));

            return mapper.Map<TestResultDetailDTO>(testResult);
        }

        public async Task<CreatedTestResultDTO> CreateTestResult(NewTestResultDTO newTestResultDTO)
        {
            TestResult testResult = mapper.Map<TestResult>(newTestResultDTO);

            testResultRepository.Insert(testResult);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestResultDTO>(testResult);
        }

        public async Task<DeletedTestResultDTO> DeleteTestResult(int testResultId)
        {
            TestResult testResult = await testResultRepository.GetByIdAsync(testResultId);
            if (testResult == null)
            {
                return null;
            }

            testResultRepository.Delete(testResult);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedTestResultDTO>(testResult);
        }

        public IEnumerable<ResultAnswerFromResultDTO> GetAnswersByResultId(int testResultId)
        {
            IEnumerable<ResultAnswer> resultAnswers = resultAnswerRepository.Get(filter: ra => ra.ResultId == testResultId);

            return mapper.Map<IEnumerable<ResultAnswerFromResultDTO>>(resultAnswers);
        }
    }
}
