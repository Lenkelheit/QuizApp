using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.DTO.Test;
using QuizApp.BLL.DTO.TestQuestion;
using QuizApp.BLL.DTO.Url;
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
            this.testService = testService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<TestDTO>> Get()
        {
            return Ok(testService.GetTests());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestDetailDTO>> GetTestById(int id)
        {
            TestDetailDTO testDetailDTO = await testService.GetTestById(id);

            if (testDetailDTO == null)
            {
                return BadRequest();
            }
            return Ok(testDetailDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedTestDTO>> Post([FromBody] NewTestDTO newTestDTO)
        {
            if (newTestDTO == null)
            {
                return BadRequest();
            }
            return Ok(await testService.CreateTest(newTestDTO));
        }

        [HttpPut]
        public async Task<ActionResult<UpdatedTestDTO>> Put([FromBody] UpdatedTestDTO updatedTestDTO)
        {
            updatedTestDTO = await testService.UpdateTest(updatedTestDTO);

            if (updatedTestDTO == null)
            {
                return BadRequest();
            }
            return Ok(updatedTestDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedTestDTO deletedTestDTO = await testService.DeleteTest(id);

            if (deletedTestDTO == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet("{id}/questions")]
        public ActionResult<IEnumerable<TestQuestionDTO>> GetQuestionsByTestId(int testId)
        {
            return Ok(testService.GetQuestionsByTestId(testId));
        }

        [HttpGet("{id}/urls")]
        public ActionResult<IEnumerable<UrlDTO>> GetUrlsByTestId(int testId)
        {
            return Ok(testService.GetUrlsByTestId(testId));
        }
    }
}
