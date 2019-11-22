﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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


        public IEnumerable<TestResultDto> GetTestResults(string intervieweeNameFilter)
        {
            IEnumerable<TestResult> results = !string.IsNullOrWhiteSpace(intervieweeNameFilter) ?
                    testResultRepository.Get(filter: r => r.IntervieweeName.Contains(intervieweeNameFilter, StringComparison.OrdinalIgnoreCase)) :
                    testResultRepository.Get();

            return mapper.Map<IEnumerable<TestResultDto>>(results);
        }

        public async Task<TestResultDetailDto> GetTestResultById(int testResultId)
        {
            TestResult testResult = await testResultRepository.GetByIdAsync(id: testResultId, includeProperties: prop => prop
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

        public ResultAnswersApiDto GetAnswersByResultId(int testResultId, int page, int amountAnswersPerPage)
        {
            ResultAnswersApiDto resultAnswersApi = new ResultAnswersApiDto();
            IEnumerable<ResultAnswer> resultAnswers = resultAnswerRepository.GetPageWithAmount(filter: ra => ra.ResultId == testResultId, page, amountAnswersPerPage, includeProperties: prop => prop
                .Include(ra => ra.ResultAnswerOptions)
                .Include(ra => ra.Question)
                    .ThenInclude(q => q.TestQuestionOptions)).ToList();

            resultAnswersApi.ResultAnswers = mapper.Map<List<ResultAnswerFromResultDto>>(resultAnswers);
            resultAnswersApi.TotalCount = resultAnswerRepository.Count(predicate: ra => ra.ResultId == testResultId);

            return resultAnswersApi;
        }
    }
}
