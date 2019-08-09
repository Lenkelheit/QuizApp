using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.DTO.ResultAnswer;
using QuizApp.BLL.DTO.TestResult;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class TestResultsController : ControllerBase
    {
        private readonly ITestResultService testResultService;


        public TestResultsController(ITestResultService testResultService)
        {
            this.testResultService = testResultService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TestResultDetailDTO>> GetTestResultById(int id)
        {
            TestResultDetailDTO testResultDetailDTO = await testResultService.GetTestResultById(id);

            if (testResultDetailDTO == null)
            {
                return BadRequest();
            }
            return Ok(testResultDetailDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedTestResultDTO>> Post([FromBody] NewTestResultDTO newTestResultDTO)
        {
            if (newTestResultDTO == null)
            {
                return BadRequest();
            }
            return Ok(await testResultService.CreateTestResult(newTestResultDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedTestResultDTO deletedTestResultDTO = await testResultService.DeleteTestResult(id);

            if (deletedTestResultDTO == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet("{id}/answers")]
        public ActionResult<IEnumerable<ResultAnswerFromResultDTO>> GetAnswersByResultId(int testResultId)
        {
            return Ok(testResultService.GetAnswersByResultId(testResultId));
        }
    }
}
