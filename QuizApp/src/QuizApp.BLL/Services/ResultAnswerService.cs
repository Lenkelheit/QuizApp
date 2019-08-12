using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.ResultAnswerOption;
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
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.resultAnswerRepository = unitOfWork.GetRepository<ResultAnswer, IResultAnswerRepository>() ?? throw new NullReferenceException(nameof(resultAnswerRepository));
            this.resultAnswerOptionRepository = unitOfWork.GetRepository<ResultAnswerOption, IResultAnswerOptionRepository>() ?? throw new NullReferenceException(nameof(resultAnswerOptionRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<ResultAnswerDetailDto> GetResultAnswerById(int resultAnswerId)
        {
            ResultAnswer resultAnswer = await resultAnswerRepository.GetByIdAsync(id: resultAnswerId, includeProperties: prop => prop
                .Include(ra => ra.ResultAnswerOptions));

            return mapper.Map<ResultAnswerDetailDto>(resultAnswer);
        }

        public async Task<CreatedResultAnswerDto> CreateResultAnswer(NewResultAnswerDto newResultAnswerDto)
        {
            ResultAnswer resultAnswer = mapper.Map<ResultAnswer>(newResultAnswerDto);

            resultAnswerRepository.Insert(resultAnswer);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedResultAnswerDto>(resultAnswer);
        }

        public async Task<DeletedResultAnswerDto> DeleteResultAnswer(int resultAnswerId)
        {
            ResultAnswer resultAnswer = await resultAnswerRepository.GetByIdAsync(resultAnswerId);
            if (resultAnswer == null)
            {
                return null;
            }

            resultAnswerRepository.Delete(resultAnswer);
            await unitOfWork.SaveAsync();

            return mapper.Map<DeletedResultAnswerDto>(resultAnswer);
        }

        public IEnumerable<ResultAnswerOptionDto> GetAnswerOptionsByResultAnswerId(int resultAnswerId)
        {
            IEnumerable<ResultAnswerOption> resultAnswerOptions = resultAnswerOptionRepository.Get(filter: opt => opt.ResultAnswerId == resultAnswerId);

            return mapper.Map<IEnumerable<ResultAnswerOptionDto>>(resultAnswerOptions);
        }
    }
}
