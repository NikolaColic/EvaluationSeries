using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationSeries.Services.Gateway.Controllers
{
    [Route("gateway/series")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private ISeriesServicesGateway _series;
        private IEvaluationServicesGateway _evaluation;

        public SeriesController(ISeriesServicesGateway series, IEvaluationServicesGateway evaluation)
        {
            this._series = series;
            this._evaluation = evaluation;
        }

        [HttpGet(Name = "GetSeries")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Series>>> GetAllSeries()
        {
            var series = await _series.GetAllSeries();
            if (series is null) return NotFound();
            return Ok(series);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Series>> GetSeriesById(int id)
        {
            var series = await _series.GetSeriesById(id);
            if (series is null) return NotFound();
            return Ok(series);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Series>>> AddSeries([FromBody] Series series)
        {
            var response = await _series.AddSeries(series);
            if (!response)  return NotFound();
            var response2 = await _evaluation.AddSeries(series);
            if (!response2) return NotFound();
            return RedirectToRoute("GetSeries");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Series>>> UpdateSeries(int id,[FromBody] Series series)
        {
            var seriesUpdate = await _series.GetSeriesById(id);
            if (seriesUpdate is null) return NotFound();

            series.Id = id;
            var response = await _series.UpdateSeries(series);
            if (!response) return NotFound();

            var response1 = await _evaluation.UpdateSeries(series, seriesUpdate);
            if (!response1) return NotFound();

            return RedirectToRoute("GetSeries");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSeries(int id)
        {
            var seriesDelete = await _series.GetSeriesById(id);
            if (seriesDelete is null) return NotFound();

            var response = await _series.DeleteSeries(id);
            if (!response) return NotFound();

            var response1 = await _evaluation.DeleteSeries(seriesDelete);
            if (!response1) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/roles", Name ="GetRoles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles(int id)
        {
            var roles = await _series.GetRoles(id);
            if (roles is null) return NotFound();
            return Ok(roles);
        }
        
        [HttpPost("{id}/roles")]
        public async Task<ActionResult<IEnumerable<Role>>> AddRole(int id, [FromBody] List<Role> roles)
        {   
            foreach(var role in roles)
            {
                var response = await _series.AddRole(role);
                if (!response ) return NotFound();
            }
            return RedirectToRoute("GetRoles", new { id = $"{id}" });
        }

        [HttpDelete("{id}/roles/{roleId}")]
        public async Task<ActionResult<IEnumerable<Role>>> DeleteRole(int id, int roleId)
        {
            var response = await _series.DeleteRole(id, roleId); 
            if (!response) return NotFound();
            return NoContent();
        }

    }
}
