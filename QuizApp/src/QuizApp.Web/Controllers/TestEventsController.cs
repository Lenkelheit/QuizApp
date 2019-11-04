using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.PassingTest;
using QuizApp.BLL.Dto.TestEvent;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Route("api/test-event")]
    [ApiController]
    public class TestEventsController : ControllerBase
    {
        private readonly ITestEventService testEventService;


        public TestEventsController(ITestEventService testEventService)
        {
            this.testEventService = testEventService ?? throw new ArgumentNullException(nameof(testEventService));
        }


        [HttpGet("session-id")]
        public ActionResult<Guid> GenerateSessionId()
        {
            return Ok(testEventService.GenerateSessionId());
        }

        [HttpPost]
        public async Task<ActionResult<CreatedTestEventDto>> Post([FromBody] NewTestEventDto newTestEventDto)
        {
            if (newTestEventDto == null)
            {
                return BadRequest();
            }

            return Ok(await testEventService.CreateTestEvent(newTestEventDto));
        }
    }
}
