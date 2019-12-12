using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.ResultAnswerOption;
using QuizApp.BLL.Dto.TestQuestionOption;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Authorize]
    [Route("api/question-options")]
    [ApiController]
    public class TestQuestionOptionsController : ControllerBase
    {
        private readonly ITestQuestionOptionService questionOptionService;


        public TestQuestionOptionsController(ITestQuestionOptionService questionOptionService)
        {
            this.questionOptionService = questionOptionService ?? throw new ArgumentNullException(nameof(questionOptionService));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TestQuestionOptionDetailDto>> GetQuestionOptionById(int id)
        {
            TestQuestionOptionDetailDto questionOptionDetailDto = await questionOptionService.GetQuestionOptionById(id);

            if (questionOptionDetailDto == null)
            {
                return NotFound();
            }

            return Ok(questionOptionDetailDto);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedTestQuestionOptionDto>> Post([FromBody] NewTestQuestionOptionDto newQuestionOptionDto)
        {
            if (newQuestionOptionDto == null)
            {
                return BadRequest();
            }

            return Ok(await questionOptionService.CreateQuestionOption(newQuestionOptionDto));
        }

        [HttpPut]
        public async Task<ActionResult<UpdatedTestQuestionOptionDto>> Put([FromBody] UpdateTestQuestionOptionDto updateQuestionOptionDto)
        {
            UpdatedTestQuestionOptionDto updatedQuestionOptionDto = await questionOptionService.UpdateQuestionOption(updateQuestionOptionDto);

            if (updatedQuestionOptionDto == null)
            {
                return NotFound();
            }

            return Ok(updatedQuestionOptionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedTestQuestionOptionDto deletedQuestionOptionDto = await questionOptionService.DeleteQuestionOption(id);

            if (deletedQuestionOptionDto == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{questionOptionId}/answer-options")]
        public ActionResult<IEnumerable<ResultAnswerOptionFromQuestionOptionDto>> GetAnswerOptionsByQuestionOptionId(int questionOptionId)
        {
            return Ok(questionOptionService.GetAnswerOptionsByQuestionOptionId(questionOptionId));
        }
    }
}
