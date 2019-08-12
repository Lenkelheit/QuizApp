using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.BLL.Dto.TestQuestionOption;
using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.BLL.Services
{
    public class TestQuestionService : ITestQuestionService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ITestQuestionRepository testQuestionRepository;

        private readonly ITestQuestionOptionRepository questionOptionRepository;

        private readonly IResultAnswerRepository resultAnswerRepository;

        private readonly IMapper mapper;


        public TestQuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.testQuestionRepository = unitOfWork.GetRepository<TestQuestion, ITestQuestionRepository>() ?? throw new NullReferenceException(nameof(testQuestionRepository));
            this.questionOptionRepository = unitOfWork.GetRepository<TestQuestionOption, ITestQuestionOptionRepository>() ?? throw new NullReferenceException(nameof(questionOptionRepository));
            this.resultAnswerRepository = unitOfWork.GetRepository<ResultAnswer, IResultAnswerRepository>() ?? throw new NullReferenceException(nameof(resultAnswerRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<TestQuestionDetailDto> GetQuestionById(int questionId)
        {
            TestQuestion testQuestion = await testQuestionRepository.GetByIdAsync(id: questionId, includeProperties: prop => prop
                .Include(q => q.TestQuestionOptions)
                .Include(q => q.ResultAnswers));

            return mapper.Map<TestQuestionDetailDto>(testQuestion);
        }

        public async Task<CreatedTestQuestionDto> CreateQuestion(NewTestQuestionDto newTestQuestionDto)
        {
            TestQuestion testQuestion = mapper.Map<TestQuestion>(newTestQuestionDto);

            testQuestionRepository.Insert(testQuestion);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestQuestionDto>(testQuestion);
        }

        public async Task<UpdatedTestQuestionDto> UpdateQuestion(UpdateTestQuestionDto updateTestQuestionDto)
        {
            TestQuestion testQuestion = await testQuestionRepository.GetByIdAsync(updateTestQuestionDto.Id);
            if (testQuestion == null)
            {
                return null;
            }

            testQuestion = mapper.Map<TestQuestion>(updateTestQuestionDto);

            testQuestionRepository.Update(testQuestion);
            await unitOfWork.SaveAsync();

            return mapper.Map<UpdatedTestQuestionDto>(testQuestion);
        }

        public async Task<DeletedTestQuestionDto> DeleteQuestion(int questionId)
        {
            TestQuestion testQuestion = await testQuestionRepository.GetByIdAsync(questionId);
            if (testQuestion == null)
            {
                return null;
            }

            testQuestionRepository.Delete(testQuestion);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedTestQuestionDto>(testQuestion);
        }

        public IEnumerable<TestQuestionOptionDto> GetQuestionOptionsByQuestionId(int questionId)
        {
            IEnumerable<TestQuestionOption> testQuestionOptions = questionOptionRepository.Get(filter: opt => opt.QuestionId == questionId);

            return mapper.Map<IEnumerable<TestQuestionOptionDto>>(testQuestionOptions);
        }

        public IEnumerable<ResultAnswerFromQuestionDto> GetResultAnswersByQuestionId(int questionId)
        {
            IEnumerable<ResultAnswer> resultAnswers = resultAnswerRepository.Get(filter: ra => ra.QuestionId == questionId);

            return mapper.Map<IEnumerable<ResultAnswerFromQuestionDto>>(resultAnswers);
        }
    }
}
