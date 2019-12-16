using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.UrlValidator;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/url-validator")]
    [ApiController]
    public class UrlValidatorController : ControllerBase
    {
        private readonly IUrlValidatorService urlValidatorService;


        public UrlValidatorController(IUrlValidatorService urlValidatorService)
        {
            this.urlValidatorService = urlValidatorService ?? throw new ArgumentNullException(nameof(urlValidatorService));
        }


        [HttpGet("{urlId}")]
        public async Task<ActionResult<UrlValidationResultDto>> CheckIsUrlValid(int urlId)
        {
            var isUrlValidResult = await urlValidatorService.CheckIsUrlValid(urlId);

            return Ok(isUrlValidResult);
        }

        [HttpPost("identify-user")]
        public async Task<ActionResult<UserIdentificationResultDto>> IdentifyUser([FromBody] IdentityUrlDto urlDto)
        {
            if (urlDto == null)
            {
                return BadRequest();
            }

            return Ok(await urlValidatorService.IdentifyUser(urlDto));
        }
    }
}
