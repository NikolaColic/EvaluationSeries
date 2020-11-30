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
    [Route("gateway/series/actors")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private ISeriesServicesGateway _actor;

        public ActorController(ISeriesServicesGateway actor)
        {
            //var httpHandler = new HttpClientHandler();
            //httpHandler.ServerCertificateCustomValidationCallback =
            //    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            //var channel = GrpcChannel.ForAddress("https://localhost:5000");
            //this._actor = new SeriesServicesGateway(new SeriesGrpc.SeriesGrpcClient(channel));
            this._actor = actor;

        }

        [HttpGet(Name = "GetActors")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAllActors()
        {
            var actors = await _actor.GetActors();
            if (actors is null) return NotFound();
            return Ok(actors);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Actor>>> AddActor([FromBody] Actor actor)
        {
            var response = await _actor.AddActor(actor);
            if (response) return RedirectToRoute("GetActors");
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Actor>>> UpdateActor(int id, [FromBody] Actor actor)
        {
            actor.ActorId = id;
            var response = await _actor.UpdateActor(actor);
            if (response) return RedirectToRoute("GetActors");
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(int id)
        {
            var response = await _actor.DeleteActor(id);
            if (response) return NoContent();
            return NotFound();
        }
    }
}
