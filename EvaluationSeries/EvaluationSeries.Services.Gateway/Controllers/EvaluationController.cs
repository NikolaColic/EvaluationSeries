using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Models;
using EvaluationSeries.Services.Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationSeries.Services.Gateway.Controllers
{
    [Route("gateway/evaluations")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private IEvaluationServicesGateway _evaluation;
        public EvaluationController(IEvaluationServicesGateway evaluation)
        {
            _evaluation = evaluation;
        }
        [HttpGet(Name ="GetEvaluations")]
        public async Task<ActionResult<IEnumerable<Evaluation>>> GetEvaluations()
        {
            var evaluations = await _evaluation.GetAllEvaluations();
            if (evaluations is null) return NotFound();
            return Ok(evaluations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluation>> GetEvaluationById(int id)
        {
            var evaluation = await _evaluation.GetEvaluationById(id);
            if (evaluation is null) return NotFound();
            return Ok(evaluation);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Evaluation>>> PostEvaluation([FromBody] EvaluationCreate evaluation)
        {
            var response = await _evaluation.AddEvaluation(evaluation);
            if (!response) return NotFound();
            return RedirectToRoute("GetEvaluations");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvaluation(int id)
        {
            var response = await _evaluation.DeleteEvaluation(id);
            if (!response) return NotFound();
            return NoContent();
        }
    }
}
