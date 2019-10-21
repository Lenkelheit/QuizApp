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
            UserIdentificationResultDto isUserIdentifiedResult = await passingTestService.IdentifyUser(urlDto);

            return Ok(isUserIdentifiedResult);
        }
    }
}
