using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.ResultAnswerOption;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Authorize]
    [Route("api/answer-options")]
    [ApiController]
    public class ResultAnswerOptionsController : ControllerBase
    {
        private readonly IResultAnswerOptionService answerOptionService;


        public ResultAnswerOptionsController(IResultAnswerOptionService answerOptionService)
        {
            this.answerOptionService = answerOptionService ?? throw new ArgumentNullException(nameof(answerOptionService));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ResultAnswerOptionDetailDto>> GetAnswerOptionById(int id)
        {
            ResultAnswerOptionDetailDto answerOptionDetailDto = await answerOptionService.GetAnswerOptionById(id);

            if (answerOptionDetailDto == null)
            {
                return NotFound();
            }

            return Ok(answerOptionDetailDto);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedResultAnswerOptionDto>> Post([FromBody] NewResultAnswerOptionDto newAnswerOptionDto)
        {
            if (newAnswerOptionDto == null)
            {
                return BadRequest();
            }

            return Ok(await answerOptionService.CreateAnswerOption(newAnswerOptionDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedResultAnswerOptionDto deletedAnswerOptionDto = await answerOptionService.DeleteAnswerOption(id);

            if (deletedAnswerOptionDto == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
