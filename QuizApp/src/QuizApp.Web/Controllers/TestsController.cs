using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.Test;
using QuizApp.BLL.Dto.TestQuestion;
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


        [HttpGet]
        public ActionResult<IEnumerable<TestDto>> Get()
        {
            return Ok(testService.GetTests());
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

        [HttpGet("{id}/questions")]
        public ActionResult<IEnumerable<TestQuestionDto>> GetQuestionsByTestId(int testId)
        {
            return Ok(testService.GetQuestionsByTestId(testId));
        }

        [HttpGet("{id}/urls")]
        public ActionResult<IEnumerable<UrlDto>> GetUrlsByTestId(int testId)
        {
            return Ok(testService.GetUrlsByTestId(testId));
        }
    }
}
