using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.DTO.ResultAnswerOption;
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
            this.unitOfWork = unitOfWork ?? throw new NullReferenceException("UnitOfWork is null.");
            this.resultAnswerOptionRepository = unitOfWork.GetRepository<ResultAnswerOption, IResultAnswerOptionRepository>();
            this.mapper = mapper;
        }


        public async Task<ResultAnswerOptionDetailDTO> GetAnswerOptionById(int answerOptionId)
        {
            ResultAnswerOption answerOption = await resultAnswerOptionRepository.GetByIdAsync(id: answerOptionId);

            return mapper.Map<ResultAnswerOptionDetailDTO>(answerOption);
        }

        public async Task<CreatedResultAnswerOptionDTO> CreateAnswerOption(NewResultAnswerOptionDTO newAnswerOptionDTO)
        {
            ResultAnswerOption answerOption = mapper.Map<ResultAnswerOption>(newAnswerOptionDTO);

            resultAnswerOptionRepository.Insert(answerOption);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedResultAnswerOptionDTO>(answerOption);
        }

        public async Task<DeletedResultAnswerOptionDTO> DeleteAnswerOption(int answerOptionId)
        {
            ResultAnswerOption answerOption = await resultAnswerOptionRepository.GetByIdAsync(answerOptionId);
            if (answerOption == null)
            {
                return null;
            }

            resultAnswerOptionRepository.Delete(answerOption);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedResultAnswerOptionDTO>(answerOption);
        }
    }
}
