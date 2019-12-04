using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.BLL.Dto.Test;
using QuizApp.BLL.Dto.TestResult;
using QuizApp.BLL.Dto.Url;
using QuizApp.BLL.Interfaces;
using QuizApp.Entities;

namespace QuizApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlService urlService;


        public UrlsController(IUrlService urlService)
        {
            this.urlService = urlService ?? throw new ArgumentNullException(nameof(urlService));
        }


        [HttpGet]
        public ActionResult<UrlsApiDto> Get(int page = 0, int amountUrlsPerPage = 10)
        {
            return Ok(urlService.GetUrls(page, amountUrlsPerPage));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UrlDetailDto>> GetUrlById(int id)
        {
            UrlDetailDto urlDetailDto = await urlService.GetUrlById(id);

            if (urlDetailDto == null)
            {
                return NotFound();
            }

            return Ok(urlDetailDto);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedUrlDto>> Post([FromBody] NewUrlDto newUrlDto)
        {
            if (newUrlDto == null)
            {
                return BadRequest();
            }

            return Ok(await urlService.CreateUrl(newUrlDto));
        }

        [HttpPut]
        public async Task<ActionResult<UpdatedUrlDto>> Put([FromBody] UpdateUrlDto updateUrlDto)
        {
            UpdatedUrlDto updatedUrlDto = await urlService.UpdateUrl(updateUrlDto);

            if (updatedUrlDto == null)
            {
                return NotFound();
            }

            return Ok(updatedUrlDto);
        }

        [HttpGet("{urlId}/test")]
        public ActionResult<TestPreviewDto> GetTestByUrlId(int urlId)
        {
            TestPreviewDto testPreviewDto = urlService.GetTestByUrlId(urlId);

            if (testPreviewDto == null)
            {
                return NotFound();
            }

            return Ok(testPreviewDto);
        }

        [HttpGet("{urlId}/results")]
        public ActionResult<TestResultsApiDto> GetTestResultsByUrlId(int urlId, int page = 0, int amountResultsPerPage = 10)
        {
            return Ok(urlService.GetTestResultsByUrlId(urlId, page, amountResultsPerPage));
        }
    }
}
