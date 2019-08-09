using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.DTO.ResultAnswerOption;
using QuizApp.BLL.DTO.TestQuestionOption;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Route("api/questinOptions")]
    [ApiController]
    public class TestQuestionOptionsController : ControllerBase
    {
        private readonly ITestQuestionOptionService questionOptionService;


        public TestQuestionOptionsController(ITestQuestionOptionService questionOptionService)
        {
            this.questionOptionService = questionOptionService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TestQuestionOptionDetailDTO>> GetQuestionOptionById(int id)
        {
            TestQuestionOptionDetailDTO questionOptionDetailDTO = await questionOptionService.GetQuestionOptionById(id);

            if (questionOptionDetailDTO == null)
            {
                return BadRequest();
            }
            return Ok(questionOptionDetailDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedTestQuestionOptionDTO>> Post([FromBody] NewTestQuestionOptionDTO newQuestionOptionDTO)
        {
            if (newQuestionOptionDTO == null)
            {
                return BadRequest();
            }
            return Ok(await questionOptionService.CreateQuestionOption(newQuestionOptionDTO));
        }

        [HttpPut]
        public async Task<ActionResult<UpdatedTestQuestionOptionDTO>> Put([FromBody] UpdatedTestQuestionOptionDTO updatedQuestionOptionDTO)
        {
            updatedQuestionOptionDTO = await questionOptionService.UpdateQuestionOption(updatedQuestionOptionDTO);

            if (updatedQuestionOptionDTO == null)
            {
                return BadRequest();
            }
            return Ok(updatedQuestionOptionDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedTestQuestionOptionDTO deletedQuestionOptionDTO = await questionOptionService.DeleteQuestionOption(id);

            if (deletedQuestionOptionDTO == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet("{id}/answerOptions")]
        public ActionResult<IEnumerable<ResultAnswerOptionFromQuestionOptionDTO>> GetAnswerOptionsByQuestionOptionId(int questionOptionId)
        {
            return Ok(questionOptionService.GetAnswerOptionsByQuestionOptionId(questionOptionId));
        }
    }
}
