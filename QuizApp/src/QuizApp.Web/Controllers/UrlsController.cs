using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.DTO.TestResult;
using QuizApp.BLL.DTO.Url;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlService urlService;


        public UrlsController(IUrlService urlService)
        {
            this.urlService = urlService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UrlDetailDTO>> GetUrlById(int id)
        {
            UrlDetailDTO urlDetailDTO = await urlService.GetUrlById(id);

            if (urlDetailDTO == null)
            {
                return BadRequest();
            }
            return Ok(urlDetailDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedUrlDTO>> Post([FromBody] NewUrlDTO newUrlDTO)
        {
            if (newUrlDTO == null)
            {
                return BadRequest();
            }
            return Ok(await urlService.CreateUrl(newUrlDTO));
        }

        [HttpPut]
        public async Task<ActionResult<UpdatedUrlDTO>> Put([FromBody] UpdatedUrlDTO updatedUrlDTO)
        {
            updatedUrlDTO = await urlService.UpdateUrl(updatedUrlDTO);

            if (updatedUrlDTO == null)
            {
                return BadRequest();
            }
            return Ok(updatedUrlDTO);
        }

        [HttpGet("{id}/results")]
        public ActionResult<IEnumerable<TestResultDTO>> GetTestResultsByUrlId(int urlId)
        {
            return Ok(urlService.GetTestResultsByUrlId(urlId));
        }
    }
}
