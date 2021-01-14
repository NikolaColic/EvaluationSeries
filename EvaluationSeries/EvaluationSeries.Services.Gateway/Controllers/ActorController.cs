using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Models;
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
    [Route("gateway/series/actors")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private ISeriesServicesGateway _series;
        private IActorServicesGateway _actor;

        public ActorController(ISeriesServicesGateway series,IActorServicesGateway actor)
        {
            this._series = series;
            this._actor = actor;
        }

        [HttpGet(Name = "GetActors")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAllActors()
        {
            var actors = await _actor.GetActors();
            if (actors is null) return NotFound();
            return Ok(actors);
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<Actor>>> AddActor([FromBody] Actor actor)
        {
            var responseActor = await _actor.AddActor(actor);
            if (!responseActor) return NotFound();
            var response = await _series.AddActor(actor);
            if (response) return RedirectToRoute("GetActors");
            return NotFound();
        }
        

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Actor>>> UpdateActor(int id, [FromBody] Actor actor)
        {
            var actorOld = await _actor.GetActorById(id);
            if (actorOld is null) return NotFound();
            actor.ActorId = id;

            var response1 = await _actor.UpdateActor(actor);
            if (!response1) return NotFound();

            var response = await _series.UpdateActor(actorOld,actor);
            if (response) return RedirectToRoute("GetActors");
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(int id)
        {
            var actorOld = await _actor.GetActorById(id);
            if (actorOld is null) return NotFound();

            var response1 = await _actor.DeleteActor(id);
            if (!response1) return NotFound();

            var response = await _series.DeleteActor(actorOld);
            if (response) return NoContent();
            return NotFound();
        }
    }
}
