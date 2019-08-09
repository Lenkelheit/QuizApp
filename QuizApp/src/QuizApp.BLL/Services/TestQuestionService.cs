using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.DTO.ResultAnswer;
using QuizApp.BLL.DTO.TestQuestion;
using QuizApp.BLL.DTO.TestQuestionOption;
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
            this.unitOfWork = unitOfWork ?? throw new NullReferenceException("UnitOfWork is null.");
            this.testQuestionRepository = unitOfWork.GetRepository<TestQuestion, ITestQuestionRepository>();
            this.questionOptionRepository = unitOfWork.GetRepository<TestQuestionOption, ITestQuestionOptionRepository>();
            this.resultAnswerRepository = unitOfWork.GetRepository<ResultAnswer, IResultAnswerRepository>();
            this.mapper = mapper;
        }


        public async Task<TestQuestionDetailDTO> GetQuestionById(int questionId)
        {
            TestQuestion testQuestion = await testQuestionRepository.GetByIdAsync(id: questionId, includeProperties: prop => prop
                .Include(q => q.TestQuestionOptions)
                .Include(q => q.ResultAnswers));

            return mapper.Map<TestQuestionDetailDTO>(testQuestion);
        }

        public async Task<CreatedTestQuestionDTO> CreateQuestion(NewTestQuestionDTO newTestQuestionDTO)
        {
            TestQuestion testQuestion = mapper.Map<TestQuestion>(newTestQuestionDTO);

            testQuestionRepository.Insert(testQuestion);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestQuestionDTO>(testQuestion);
        }

        public async Task<UpdatedTestQuestionDTO> UpdateQuestion(UpdatedTestQuestionDTO updatedTestQuestionDTO)
        {
            TestQuestion testQuestion = await testQuestionRepository.GetByIdAsync(updatedTestQuestionDTO.Id);
            if (testQuestion == null)
            {
                return null;
            }
            testQuestion = mapper.Map<TestQuestion>(updatedTestQuestionDTO);

            testQuestionRepository.Update(testQuestion);
            await unitOfWork.SaveAsync();

            return updatedTestQuestionDTO;
        }

        public async Task<DeletedTestQuestionDTO> DeleteQuestion(int questionId)
        {
            TestQuestion testQuestion = await testQuestionRepository.GetByIdAsync(questionId);
            if (testQuestion == null)
            {
                return null;
            }

            testQuestionRepository.Delete(testQuestion);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedTestQuestionDTO>(testQuestion);
        }

        public IEnumerable<TestQuestionOptionDTO> GetQuestionOptionsByQuestionId(int questionId)
        {
            IEnumerable<TestQuestionOption> testQuestionOptions = questionOptionRepository.Get(filter: opt => opt.QuestionId == questionId);

            return mapper.Map<IEnumerable<TestQuestionOptionDTO>>(testQuestionOptions);
        }

        public IEnumerable<ResultAnswerFromQuestionDTO> GetResultAnswersByQuestionId(int questionId)
        {
            IEnumerable<ResultAnswer> resultAnswers = resultAnswerRepository.Get(filter: ra => ra.QuestionId == questionId);

            return mapper.Map<IEnumerable<ResultAnswerFromQuestionDTO>>(resultAnswers);
        }
    }
}
