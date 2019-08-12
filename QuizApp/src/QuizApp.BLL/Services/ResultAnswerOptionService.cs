using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Dto.ResultAnswerOption;
using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.BLL.Services
{
    public class ResultAnswerOptionService : IResultAnswerOptionService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IResultAnswerOptionRepository resultAnswerOptionRepository;

        private readonly IMapper mapper;


        public ResultAnswerOptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.resultAnswerOptionRepository = unitOfWork.GetRepository<ResultAnswerOption, IResultAnswerOptionRepository>() ?? throw new NullReferenceException(nameof(resultAnswerOptionRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<ResultAnswerOptionDetailDto> GetAnswerOptionById(int answerOptionId)
        {
            ResultAnswerOption answerOption = await resultAnswerOptionRepository.GetByIdAsync(id: answerOptionId);

            return mapper.Map<ResultAnswerOptionDetailDto>(answerOption);
        }

        public async Task<CreatedResultAnswerOptionDto> CreateAnswerOption(NewResultAnswerOptionDto newAnswerOptionDto)
        {
            ResultAnswerOption answerOption = mapper.Map<ResultAnswerOption>(newAnswerOptionDto);

            resultAnswerOptionRepository.Insert(answerOption);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedResultAnswerOptionDto>(answerOption);
        }

        public async Task<DeletedResultAnswerOptionDto> DeleteAnswerOption(int answerOptionId)
        {
            ResultAnswerOption answerOption = await resultAnswerOptionRepository.GetByIdAsync(answerOptionId);
            if (answerOption == null)
            {
                return null;
            }

            resultAnswerOptionRepository.Delete(answerOption);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedResultAnswerOptionDto>(answerOption);
        }
    }
}
