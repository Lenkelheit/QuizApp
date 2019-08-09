using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.DTO.ResultAnswer;
using QuizApp.BLL.DTO.ResultAnswerOption;
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
        public async Task<ActionResult<ResultAnswerDetailDTO>> GetResultAnswerById(int id)
        {
            ResultAnswerDetailDTO resultAnswerDetailDTO = await resultAnswerService.GetResultAnswerById(id);

            if (resultAnswerDetailDTO == null)
            {
                return BadRequest();
            }
            return Ok(resultAnswerDetailDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedResultAnswerDTO>> Post([FromBody] NewResultAnswerDTO newResultAnswerDTO)
        {
            if (newResultAnswerDTO == null)
            {
                return BadRequest();
            }
            return Ok(await resultAnswerService.CreateResultAnswer(newResultAnswerDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedResultAnswerDTO deletedResultAnswerDTO = await resultAnswerService.DeleteResultAnswer(id);

            if (deletedResultAnswerDTO == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet("{id}/options")]
        public ActionResult<IEnumerable<ResultAnswerOptionDTO>> GetAnswerOptionsByResultAnswerId(int resultAnswerId)
        {
            return Ok(resultAnswerService.GetAnswerOptionsByResultAnswerId(resultAnswerId));
        }
    }
}
