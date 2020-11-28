using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Series.Entities;
using EvaluationSeries.Services.Series.Help;
using EvaluationSeries.Services.Series.Models;
using EvaluationSeries.Services.Series.Repository;
using EvaluationSeries.Services.Series.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationSeries.Services.Series.Controllers
{
    [Route("services/series/actor")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private ActorServices actorServices;
        private IActorRepository _actor;
        
        public ActorController(IActorRepository actor)
        {
            this._actor = actor;
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            this.actorServices = new ActorServices(new ActorsGrpc.ActorsGrpcClient(channel));

        }

        [HttpGet(Name = "GetAllActors")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAllActors()
        {
            var actors = await actorServices.GetAll();
            if (actors is null) return NotFound();
            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Series2>> GetActorsById(int id)
        {

            var actor = await actorServices.GetActorById(id);
            if (actor is null) return NotFound();
            return Ok(actor);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Series2>>> AddSeries([FromBody] ActorCreate actor)
        {
            if (!await actorServices.PostActor(actor)) return NotFound();

            if (await _actor.AddActor(ConvertObject.Instance.CreateActor(actor))) return RedirectToAction("GetAllActors");
            return NotFound();
        }
       
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Series2>>> UpdateSeries([FromBody] ActorCreate actor)
        {
            var actorUpdate = await actorServices.GetActorById(actor.ActorId);
            if (actorUpdate is null) return NotFound();
            if (!await actorServices.PutActor(actor)) return NotFound();
            if (await _actor.UpdateActor(actorUpdate, ConvertObject.Instance.CreateActor(actor))) return RedirectToAction("GetAllActors");
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var actorDelete = await actorServices.GetActorById(id);
            if (actorDelete is null) return NotFound();
            if (!await actorServices.DeleteActor(id)) return NotFound();
            if (await _actor.DeleteActor(actorDelete)) return NoContent();
            return NotFound();
        }
    }
}
