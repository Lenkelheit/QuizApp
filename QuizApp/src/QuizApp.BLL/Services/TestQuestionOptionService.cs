using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.DTO.ResultAnswerOption;
using QuizApp.BLL.DTO.TestQuestionOption;
using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.BLL.Services
{
    public class TestQuestionOptionService : ITestQuestionOptionService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ITestQuestionOptionRepository questionOptionRepository;

        private readonly IResultAnswerOptionRepository resultAnswerOptionRepository;

        private readonly IMapper mapper;


        public TestQuestionOptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new NullReferenceException("UnitOfWork is null.");
            this.questionOptionRepository = unitOfWork.GetRepository<TestQuestionOption, ITestQuestionOptionRepository>();
            this.resultAnswerOptionRepository = unitOfWork.GetRepository<ResultAnswerOption, IResultAnswerOptionRepository>();
            this.mapper = mapper;
        }


        public async Task<TestQuestionOptionDetailDTO> GetQuestionOptionById(int questionOptionId)
        {
            TestQuestionOption questionOption = await questionOptionRepository.GetByIdAsync(id: questionOptionId, includeProperties: prop => prop
                .Include(opt => opt.ResultAnswerOptions));

            return mapper.Map<TestQuestionOptionDetailDTO>(questionOption);
        }

        public async Task<CreatedTestQuestionOptionDTO> CreateQuestionOption(NewTestQuestionOptionDTO newQuestionOptionDTO)
        {
            TestQuestionOption questionOption = mapper.Map<TestQuestionOption>(newQuestionOptionDTO);

            questionOptionRepository.Insert(questionOption);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestQuestionOptionDTO>(questionOption);
        }

        public async Task<UpdatedTestQuestionOptionDTO> UpdateQuestionOption(UpdatedTestQuestionOptionDTO updatedQuestionOptionDTO)
        {
            TestQuestionOption questionOption = await questionOptionRepository.GetByIdAsync(updatedQuestionOptionDTO.Id);
            if (questionOption == null)
            {
                return null;
            }
            questionOption = mapper.Map<TestQuestionOption>(updatedQuestionOptionDTO);

            questionOptionRepository.Update(questionOption);
            await unitOfWork.SaveAsync();

            return updatedQuestionOptionDTO;
        }

        public async Task<DeletedTestQuestionOptionDTO> DeleteQuestionOption(int questionOptionId)
        {
            TestQuestionOption questionOption = await questionOptionRepository.GetByIdAsync(questionOptionId);
            if (questionOption == null)
            {
                return null;
            }

            questionOptionRepository.Delete(questionOption);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedTestQuestionOptionDTO>(questionOption);
        }

        public IEnumerable<ResultAnswerOptionFromQuestionOptionDTO> GetAnswerOptionsByQuestionOptionId(int questionOptionId)
        {
            IEnumerable<ResultAnswerOption> resultAnswerOptions = resultAnswerOptionRepository.Get(filter: opt => opt.OptionId == questionOptionId);

            return mapper.Map<IEnumerable<ResultAnswerOptionFromQuestionOptionDTO>>(resultAnswerOptions);
        }
    }
}
