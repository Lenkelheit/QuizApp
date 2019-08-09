using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QuizApp.BLL.DTO.ResultAnswerOption;
using QuizApp.BLL.Interfaces;

namespace QuizApp.Web.Controllers
{
    [Route("api/answerOptions")]
    [ApiController]
    public class ResultAnswerOptionsController : ControllerBase
    {
        private readonly IResultAnswerOptionService answerOptionService;


        public ResultAnswerOptionsController(IResultAnswerOptionService answerOptionService)
        {
            this.answerOptionService = answerOptionService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ResultAnswerOptionDetailDTO>> GetAnswerOptionById(int id)
        {
            ResultAnswerOptionDetailDTO answerOptionDetailDTO = await answerOptionService.GetAnswerOptionById(id);

            if (answerOptionDetailDTO == null)
            {
                return BadRequest();
            }
            return Ok(answerOptionDetailDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedResultAnswerOptionDTO>> Post([FromBody] NewResultAnswerOptionDTO newAnswerOptionDTO)
        {
            if (newAnswerOptionDTO == null)
            {
                return BadRequest();
            }
            return Ok(await answerOptionService.CreateAnswerOption(newAnswerOptionDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletedResultAnswerOptionDTO deletedAnswerOptionDTO = await answerOptionService.DeleteAnswerOption(id);

            if (deletedAnswerOptionDTO == null)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
