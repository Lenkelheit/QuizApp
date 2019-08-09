using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.DTO.ResultAnswer;
using QuizApp.BLL.DTO.TestQuestion;
using QuizApp.BLL.DTO.TestQuestionOption;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class TestQuestionsController : ControllerBase
    {
        private readonly ITestQuestionService testQuestionService;


        public TestQuestionsController(ITestQuestionService testQuestionService)
        {
            this.testQuestionService = testQuestionService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TestQuestionDetailDTO>> GetQuestionById(int id)
        {
            TestQuestionDetailDTO testQuestionDetailDTO = await testQuestionService.GetQuestionById(id);

            if (testQuestionDetailDTO == null)
            {
                return BadRequest();
            }
            return Ok(testQuestionDetailDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedTestQuestionDTO>> Post([FromBody] NewTestQuestionDTO newTestQuestionDTO)
        {
            if (newTestQuestionDTO == null)
            {
                return BadRequest();
            }
            return Ok(await testQuestionService.CreateQuestion(newTestQuestionDTO));
        }

        [HttpPut]
        public async Task<ActionResult<UpdatedTestQuestionDTO>> Put([FromBody] UpdatedTestQuestionDTO updatedTestQuestionDTO)
        {
            updatedTestQuestionDTO = await testQuestionService.UpdateQuestion(updatedTestQuestionDTO);

            if (updatedTestQuestionDTO == null)
            {
                return BadRequest();
            }
            return Ok(updatedTestQuestionDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedTestQuestionDTO deletedTestQuestionDTO = await testQuestionService.DeleteQuestion(id);

            if (deletedTestQuestionDTO == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet("{id}/options")]
        public ActionResult<IEnumerable<TestQuestionOptionDTO>> GetQuestionOptionsByQuestionId(int questionId)
        {
            return Ok(testQuestionService.GetQuestionOptionsByQuestionId(questionId));
        }

        [HttpGet("{id}/answers")]
        public ActionResult<IEnumerable<ResultAnswerFromQuestionDTO>> GetResultAnswersByQuestionId(int questionId)
        {
            return Ok(testQuestionService.GetResultAnswersByQuestionId(questionId));
        }
    }
}
