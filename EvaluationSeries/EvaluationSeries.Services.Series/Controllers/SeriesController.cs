using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluationSeries.Services.Series.Entities;
using EvaluationSeries.Services.Series.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationSeries.Services.Series.Controllers
{
    [Route("services/series")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private ISeriesRepository _series;
        public SeriesController(ISeriesRepository series)
        {
            _series = series;
        }
        [HttpGet(Name = "GetAllSeries")]
        public async Task<ActionResult<IEnumerable<Series2>>> GetAllSeries()
        {
            var series = await _series.GetAllSeries();
            if (series is null) return NotFound();
            return Ok(series);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Series2>> GetSeriesById(int id)
        {
            var series = await _series.GetSeriesById(id);
            if (series is null) return NotFound();
            return Ok(series);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Series2>>> AddSeries([FromBody] Series2 series)
        {
            if (await _series.AddSeries(series)) return RedirectToAction("GetAllSeries");
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Series2>>> UpdateSeries([FromBody] Series2 series)
        {
            if (await _series.UpdateSeries(series)) return RedirectToAction("GetAllSeries");
            return NotFound();
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _series.DeleteSeries(id)) return NoContent();
            return NotFound();
        }

        //Roles 
        [HttpGet("{id}/roles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRolesSeries(int id)
        {
            var roles = await _series.GetRolesSeries(id); ;
            if (roles is null) return NotFound();
            return Ok(roles);

        }
        [HttpPost("{id}/roles")]
        public async Task<ActionResult<IEnumerable<Role>>> AddRoles(int id, [FromBody] List<Role> roles)
        {
            foreach(var r in roles)
            {
                if (!await _series.AddRole(id, r)) return NotFound();
            }
            return Ok();

        }

        [HttpDelete("{id}/roles/{roleId}")]
        public async Task<ActionResult<IEnumerable<Role>>> AddRoles(int id, int roleId)
        {
            if (await _series.DeleteRole(id, roleId)) return NoContent();
            return NotFound();

        }



    }
}
