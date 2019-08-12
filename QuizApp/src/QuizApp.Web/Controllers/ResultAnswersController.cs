using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.Dto.ResultAnswer;
using QuizApp.BLL.Dto.ResultAnswerOption;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Route("api/answers")]
    [ApiController]
    public class ResultAnswersController : ControllerBase
    {
        private readonly IResultAnswerService resultAnswerService;


        public ResultAnswersController(IResultAnswerService resultAnswerService)
        {
            this.resultAnswerService = resultAnswerService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ResultAnswerDetailDto>> GetResultAnswerById(int id)
        {
            ResultAnswerDetailDto resultAnswerDetailDto = await resultAnswerService.GetResultAnswerById(id);

            if (resultAnswerDetailDto == null)
            {
                return NotFound();
            }

            return Ok(resultAnswerDetailDto);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedResultAnswerDto>> Post([FromBody] NewResultAnswerDto newResultAnswerDto)
        {
            if (newResultAnswerDto == null)
            {
                return BadRequest();
            }

            return Ok(await resultAnswerService.CreateResultAnswer(newResultAnswerDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedResultAnswerDto deletedResultAnswerDto = await resultAnswerService.DeleteResultAnswer(id);

            if (deletedResultAnswerDto == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}/options")]
        public ActionResult<IEnumerable<ResultAnswerOptionDto>> GetAnswerOptionsByResultAnswerId(int resultAnswerId)
        {
            return Ok(resultAnswerService.GetAnswerOptionsByResultAnswerId(resultAnswerId));
        }
    }
}
