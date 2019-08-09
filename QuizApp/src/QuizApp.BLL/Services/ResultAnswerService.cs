using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.DTO.ResultAnswer;
using QuizApp.BLL.DTO.ResultAnswerOption;
using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;

namespace QuizApp.BLL.Services
{
    public class ResultAnswerService : IResultAnswerService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IResultAnswerRepository resultAnswerRepository;

        private readonly IResultAnswerOptionRepository resultAnswerOptionRepository;

        private readonly IMapper mapper;


        public ResultAnswerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new NullReferenceException("UnitOfWork is null.");
            this.resultAnswerRepository = unitOfWork.GetRepository<ResultAnswer, IResultAnswerRepository>();
            this.resultAnswerOptionRepository = unitOfWork.GetRepository<ResultAnswerOption, IResultAnswerOptionRepository>();
            this.mapper = mapper;
        }


        public async Task<ResultAnswerDetailDTO> GetResultAnswerById(int resultAnswerId)
        {
            ResultAnswer resultAnswer = await resultAnswerRepository.GetByIdAsync(id: resultAnswerId, includeProperties: prop => prop
                .Include(ra => ra.ResultAnswerOptions));

            return mapper.Map<ResultAnswerDetailDTO>(resultAnswer);
        }

        public async Task<CreatedResultAnswerDTO> CreateResultAnswer(NewResultAnswerDTO newResultAnswerDTO)
        {
            ResultAnswer resultAnswer = mapper.Map<ResultAnswer>(newResultAnswerDTO);

            resultAnswerRepository.Insert(resultAnswer);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedResultAnswerDTO>(resultAnswer);
        }

        public async Task<DeletedResultAnswerDTO> DeleteResultAnswer(int resultAnswerId)
        {
            ResultAnswer resultAnswer = await resultAnswerRepository.GetByIdAsync(resultAnswerId);
            if (resultAnswer == null)
            {
                return null;
            }

            resultAnswerRepository.Delete(resultAnswer);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedResultAnswerDTO>(resultAnswer);
        }

        public IEnumerable<ResultAnswerOptionDTO> GetAnswerOptionsByResultAnswerId(int resultAnswerId)
        {
            IEnumerable<ResultAnswerOption> resultAnswerOptions = resultAnswerOptionRepository.Get(filter: opt => opt.ResultAnswerId == resultAnswerId);

            return mapper.Map<IEnumerable<ResultAnswerOptionDTO>>(resultAnswerOptions);
        }
    }
}
