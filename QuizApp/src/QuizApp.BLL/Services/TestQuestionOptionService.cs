using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Dto.ResultAnswerOption;
using QuizApp.BLL.Dto.TestQuestionOption;
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
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.questionOptionRepository = unitOfWork.GetRepository<TestQuestionOption, ITestQuestionOptionRepository>() ?? throw new NullReferenceException(nameof(questionOptionRepository));
            this.resultAnswerOptionRepository = unitOfWork.GetRepository<ResultAnswerOption, IResultAnswerOptionRepository>() ?? throw new NullReferenceException(nameof(resultAnswerOptionRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<TestQuestionOptionDetailDto> GetQuestionOptionById(int questionOptionId)
        {
            TestQuestionOption questionOption = await questionOptionRepository.GetByIdAsync(id: questionOptionId, includeProperties: prop => prop
                .Include(opt => opt.ResultAnswerOptions));

            return mapper.Map<TestQuestionOptionDetailDto>(questionOption);
        }

        public async Task<CreatedTestQuestionOptionDto> CreateQuestionOption(NewTestQuestionOptionDto newQuestionOptionDto)
        {
            TestQuestionOption questionOption = mapper.Map<TestQuestionOption>(newQuestionOptionDto);

            questionOptionRepository.Insert(questionOption);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestQuestionOptionDto>(questionOption);
        }

        public async Task<UpdatedTestQuestionOptionDto> UpdateQuestionOption(UpdateTestQuestionOptionDto updateQuestionOptionDto)
        {
            TestQuestionOption questionOption = await questionOptionRepository.GetByIdAsync(updateQuestionOptionDto.Id);
            if (questionOption == null)
            {
                return null;
            }

            questionOption = mapper.Map<TestQuestionOption>(updateQuestionOptionDto);

            questionOptionRepository.Update(questionOption);
            await unitOfWork.SaveAsync();

            return mapper.Map<UpdatedTestQuestionOptionDto>(questionOption);
        }

        public async Task<DeletedTestQuestionOptionDto> DeleteQuestionOption(int questionOptionId)
        {
            TestQuestionOption questionOption = await questionOptionRepository.GetByIdAsync(questionOptionId);
            if (questionOption == null)
            {
                return null;
            }

            questionOptionRepository.Delete(questionOption);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedTestQuestionOptionDto>(questionOption);
        }

        public IEnumerable<ResultAnswerOptionFromQuestionOptionDto> GetAnswerOptionsByQuestionOptionId(int questionOptionId)
        {
            IEnumerable<ResultAnswerOption> resultAnswerOptions = resultAnswerOptionRepository.Get(filter: opt => opt.OptionId == questionOptionId);

            return mapper.Map<IEnumerable<ResultAnswerOptionFromQuestionOptionDto>>(resultAnswerOptions);
        }
    }
}
