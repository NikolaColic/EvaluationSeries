using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationSeries.Services.Gateway.Controllers
{
    [Route("gateway/evaluations/marks")]
    [ApiController]
    public class MarkController : ControllerBase
    {

        private IEvaluationServicesGateway _mark;
        public MarkController(IEvaluationServicesGateway mark)
        {
            _mark = mark;
        }
        [HttpGet(Name = "GetMarks")]
        public async Task<ActionResult<IEnumerable<Mark>>> GetMarks()
        {
            var marks = await _mark.GetAllMarks();
            if (marks is null) return NotFound();
            return Ok(marks);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Mark>>> PutMarks([FromBody] List<Mark> marks)
        {
            var response = await _mark.UpdateMarks(marks);
            if (!response) return NotFound();
            return RedirectToRoute("GetMarks");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMarks(int id)
        {
            var response = await _mark.DeleteMarks(id);
            if (!response) return NotFound();
            return NoContent();
        }
    }
}

