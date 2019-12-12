using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.BLL.Dto.TestQuestionOption;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Authorize]
    [Route("api/questions")]
    [ApiController]
    public class TestQuestionsController : ControllerBase
    {
        private readonly ITestQuestionService testQuestionService;


        public TestQuestionsController(ITestQuestionService testQuestionService)
        {
            this.testQuestionService = testQuestionService ?? throw new ArgumentNullException(nameof(testQuestionService));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TestQuestionDetailDto>> GetQuestionById(int id)
        {
            TestQuestionDetailDto testQuestionDetailDto = await testQuestionService.GetQuestionById(id);

            if (testQuestionDetailDto == null)
            {
                return NotFound();
            }

            return Ok(testQuestionDetailDto);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedTestQuestionDto>> Post([FromBody] NewTestQuestionDto newTestQuestionDto)
        {
            if (newTestQuestionDto == null)
            {
                return BadRequest();
            }

            return Ok(await testQuestionService.CreateQuestion(newTestQuestionDto));
        }

        [HttpPut]
        public async Task<ActionResult<UpdatedTestQuestionDto>> Put([FromBody] UpdateTestQuestionDto updateTestQuestionDto)
        {
            UpdatedTestQuestionDto updatedTestQuestionDto = await testQuestionService.UpdateQuestion(updateTestQuestionDto);

            if (updatedTestQuestionDto == null)
            {
                return NotFound();
            }

            return Ok(updatedTestQuestionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedTestQuestionDto deletedTestQuestionDto = await testQuestionService.DeleteQuestion(id);

            if (deletedTestQuestionDto == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{questionId}/options")]
        public ActionResult<IEnumerable<TestQuestionOptionDto>> GetQuestionOptionsByQuestionId(int questionId)
        {
            return Ok(testQuestionService.GetQuestionOptionsByQuestionId(questionId));
        }

        [HttpGet("{questionId}/answers")]
        public ActionResult<IEnumerable<ResultAnswerFromQuestionDto>> GetResultAnswersByQuestionId(int questionId)
        {
            return Ok(testQuestionService.GetResultAnswersByQuestionId(questionId));
        }
    }
}
