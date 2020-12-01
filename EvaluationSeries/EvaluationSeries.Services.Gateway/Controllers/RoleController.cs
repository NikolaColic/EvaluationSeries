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
    [Route("gateway/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private ISeriesServicesGateway _series;
        public RoleController(ISeriesServicesGateway series)
        {
            this._series = series;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _series.GetAllRoles();
            if (roles is null) return NotFound();
            return Ok(roles);

        }
    }
}
