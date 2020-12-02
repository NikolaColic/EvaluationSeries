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
    [Route("gateway/evaluations/criterions")]
    [ApiController]
    public class CriterionController : ControllerBase
    {
        private IEvaluationServicesGateway _criterion;
        public CriterionController(IEvaluationServicesGateway criterion)
        {
            _criterion = criterion;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluationCriterion>>> GetCriterions()
        {
            var criterions = await _criterion.GetAllCriterions();
            if (criterions is null) return NotFound();
            return Ok(criterions);
        }
    }
}
