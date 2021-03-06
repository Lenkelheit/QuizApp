﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.TestResult;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Authorize]
    [Route("api/results")]
    [ApiController]
    public class TestResultsController : ControllerBase
    {
        private readonly ITestResultService testResultService;


        public TestResultsController(ITestResultService testResultService)
        {
            this.testResultService = testResultService ?? throw new ArgumentNullException(nameof(testResultService));
        }


        [HttpGet]
        public ActionResult<TestResultsApiDto> Get(string intervieweeNameFilter, int page = 0, int amountResultsPerPage = 10)
        {
            return Ok(testResultService.GetTestResults(intervieweeNameFilter, page, amountResultsPerPage, User.Identity.Name));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<TestResultDetailDto>> GetTestResultById(int id)
        {
            TestResultDetailDto testResultDetailDto = await testResultService.GetTestResultById(id);

            if (testResultDetailDto == null)
            {
                return NotFound();
            }

            return Ok(testResultDetailDto);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedTestResultDto>> Post([FromBody] NewTestResultDto newTestResultDto)
        {
            if (newTestResultDto == null)
            {
                return BadRequest();
            }

            return Ok(await testResultService.CreateTestResult(newTestResultDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedTestResultDto deletedTestResultDto = await testResultService.DeleteTestResult(id);

            if (deletedTestResultDto == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("{testResultId}/answers")]
        public ActionResult<ResultAnswersApiDto> GetAnswersByResultId(int testResultId, int page = 0, int amountAnswersPerPage = 3)
        {
            return Ok(testResultService.GetAnswersByResultId(testResultId, page, amountAnswersPerPage));
        }
    }
}
