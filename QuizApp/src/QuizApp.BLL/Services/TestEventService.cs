using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using QuizApp.BLL.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.Entities;
using QuizApp.BLL.Dto.TestEvent;

namespace QuizApp.BLL.Services
{
    public class TestEventService : ITestEventService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly ITestEventRepository testEventRepository;

        private readonly IMapper mapper;


        public TestEventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.testEventRepository = unitOfWork.GetRepository<TestEvent, ITestEventRepository>() ?? throw new NullReferenceException(nameof(testEventRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public Guid GenerateSessionId()
        {
            return Guid.NewGuid();
        }

        public async Task<CreatedTestEventDto> CreateTestEvent(NewTestEventDto newTestEventDto)
        {
            TestEvent testEvent = mapper.Map<TestEvent>(newTestEventDto);

            testEvent.StartTime = DateTime.Now;
            testEventRepository.Insert(testEvent);
            await unitOfWork.SaveAsync();

            return mapper.Map<CreatedTestEventDto>(testEvent);
        }
    }
}
