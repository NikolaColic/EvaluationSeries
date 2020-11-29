using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Services;
using Grpc.Net.Client;
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
        private SeriesServicesGateway _series;

        public SeriesController()
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5000");
            this._series = new SeriesServicesGateway(new SeriesGrpc.SeriesGrpcClient(channel));
        }

        [HttpGet(Name = "GetSeries")]
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
            if (response) return RedirectToRoute("GetSeries");
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Series>>> UpdateSeries([FromBody] Series series)
        {
            var response = await _series.UpdateSeries(series);
            if (response) return RedirectToRoute("GetSeries");
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSeries(int id)
        {
            var response = await _series.DeleteSeries(id);
            if (response) return NoContent();
            return NotFound();
        }

        [HttpGet("{id}/roles", Name ="GetRoles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles(int id)
        {
            var roles = await _series.GetRoles(id);
            if (roles is null) return NotFound();
            return Ok(roles);
        }
        
        [HttpPost("{id}/roles")]
        public async Task<ActionResult<IEnumerable<Role>>> AddRole(int id, [FromBody] Role role)
        {
            var response = await _series.AddRole(role);
            if (!response ) return NotFound();
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
