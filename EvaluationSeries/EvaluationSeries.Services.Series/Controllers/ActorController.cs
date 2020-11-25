using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Series.Entities;
using EvaluationSeries.Services.Series.Models;
using EvaluationSeries.Services.Series.Repository;
using EvaluationSeries.Services.Series.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationSeries.Services.Series.Controllers
{
    [Route("services/series/actor")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private ActorServices actorServices;
        //private IActorRepository _actor;
        //private IActorServices _service;
        //public ActorController(IActorRepository actor, IActorServices service)
        //{
        //    _actor = actor;
        //    _service = service;
        //}
        public ActorController()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:44344/");
            this.actorServices = new ActorServices(new ActorsGrpc.ActorsGrpcClient(channel));
        }
        [HttpGet(Name = "GetAllActors")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAllActors()
        {
            //var actors = await _service.GetAll();
            //if (actors is null) return NotFound();
            //return Ok(actors);
            var actors = await actorServices.GetAll();
            return null;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Series2>> GetActorsById(int id)
        {
            var actor = await actorServices.GetActorById(id);
            if (actor is null) return NotFound();
            return Ok(actor);
        }

        //[HttpPost]
        //public async Task<ActionResult<IEnumerable<Series2>>> AddSeries([FromBody] ActorCreate actor)
        //{
        //    if (!await _service.PostActor(actor)) return NotFound();

        //    if (await _actor.AddActor(CreateActor(actor))) return RedirectToAction("GetAllActors");
        //    return NotFound();
        //}
        //private Actor CreateActor(ActorCreate actor)
        //{
        //    Actor a = new Actor()
        //    {
        //        Name = actor.Name,
        //        Surname = actor.Surname
        //    };
        //    return a;
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<IEnumerable<Series2>>> UpdateSeries([FromBody] ActorCreate actor)
        //{
        //    var actorUpdate = await _service.GetActorById(actor.ActorId);
        //    if (actorUpdate is null) return NotFound();
        //    if (!await _service.PutActor(actor)) return NotFound();
        //    if (await _actor.UpdateActor(actorUpdate, CreateActor(actor))) return RedirectToAction("GetAllActors");
        //    return NotFound();
        //}


        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var actorDelete = await _service.GetActorById(id);
        //    if (actorDelete is null) return NotFound();
        //    if (!await _service.DeleteActor(id)) return NotFound();
        //    if (await _actor.DeleteActor(actorDelete)) return NoContent();
        //    return NotFound();
        //}
    }
}
