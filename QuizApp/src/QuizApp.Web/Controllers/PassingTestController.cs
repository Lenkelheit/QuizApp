using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.PassingTest;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Route("api/passing-test")]
    [ApiController]
    public class PassingTestController : ControllerBase
    {
        private readonly IPassingTestService passingTestService;


        public PassingTestController(IPassingTestService passingTestService)
        {
            this.passingTestService = passingTestService ?? throw new ArgumentNullException(nameof(passingTestService));
        }


        [HttpGet("{urlId}")]
        public async Task<ActionResult<UrlValidationResultDto>> CheckIsUrlValid(int urlId)
        {
            UrlValidationResultDto isUrlValidResult = await passingTestService.CheckIsUrlValid(urlId);

            return Ok(isUrlValidResult);
        }

        [HttpPost("identify-user")]
        public async Task<ActionResult<UserIdentificationResultDto>> IdentifyUser([FromBody] IdentityUrlDto urlDto)
        {
            if (urlDto == null)
            {
                return BadRequest();
            }

            return Ok(await passingTestService.IdentifyUser(urlDto));
        }

        [HttpGet("test/{testId}")]
        public async Task<ActionResult<ViewTestDto>> GetTestById(int testId)
        {
            ViewTestDto viewTestDto = await passingTestService.GetTestById(testId);

            if (viewTestDto == null)
            {
                return NotFound();
            }

            return Ok(viewTestDto);
        }

        [HttpPost("test-result")]
        public async Task<ActionResult<CreatedTestResultDto>> CreateTestResult([FromBody] UserUrlDto userUrlDto)
        {
            if (userUrlDto == null)
            {
                return BadRequest();
            }

            return Ok(await passingTestService.CreateTestResult(userUrlDto));
        }
    }
}
