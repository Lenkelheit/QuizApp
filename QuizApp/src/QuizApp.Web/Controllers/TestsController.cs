using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.Test;
using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.BLL.Dto.TestResult;
using QuizApp.BLL.Dto.Url;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestService testService;


        public TestsController(ITestService testService)
        {
            this.testService = testService ?? throw new ArgumentNullException(nameof(testService));
        }


        [Authorize]
        [HttpGet]
        public ActionResult<TestsApiDto> Get(int page = 0, int amountTestsPerPage = 10)
        {
            return Ok(testService.GetTests(page, amountTestsPerPage));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestDetailDto>> GetTestById(int id)
        {
            TestDetailDto testDetailDto = await testService.GetTestById(id);

            if (testDetailDto == null)
            {
                return NotFound();
            }

            return Ok(testDetailDto);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedTestDto>> Post([FromBody] NewTestDto newTestDto)
        {
            if (newTestDto == null)
            {
                return BadRequest();
            }

            return Ok(await testService.CreateTest(newTestDto));
        }

        [HttpPut]
        public async Task<ActionResult<UpdatedTestDto>> Put([FromBody] UpdateTestDto updateTestDto)
        {
            UpdatedTestDto updatedTestDto = await testService.UpdateTest(updateTestDto);

            if (updatedTestDto == null)
            {
                return NotFound();
            }

            return Ok(updatedTestDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedTestDto deletedTestDto = await testService.DeleteTest(id);

            if (deletedTestDto == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{testId}/questions")]
        public ActionResult<IEnumerable<TestQuestionDto>> GetQuestionsByTestId(int testId)
        {
            return Ok(testService.GetQuestionsByTestId(testId));
        }

        [HttpGet("{testId}/urls")]
        public ActionResult<UrlsApiDto> GetUrlsByTestId(int testId, int page = 0, int amountUrlsPerPage = 10)
        {
            return Ok(testService.GetUrlsByTestId(testId, page, amountUrlsPerPage));
        }

        [HttpGet("{testId}/results")]
        public ActionResult<TestResultsApiDto> GetResultsByTestId(int testId, int page = 0, int amountResultsPerPage = 10)
        {
            return Ok(testService.GetResultsByTestId(testId, page, amountResultsPerPage));
        }

        [HttpGet("passing-test/{testId}")]
        public async Task<ActionResult<ViewTestDto>> GetPassingTestById(int testId)
        {
            var viewTestDto = await testService.GetPassingTestById(testId);

            if (viewTestDto == null)
            {
                return NotFound();
            }

            return Ok(viewTestDto);
        }
    }
}
