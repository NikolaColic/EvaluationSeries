using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EvaluationSeries.Services.Actors.Entities;
using EvaluationSeries.Services.Actors.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationSeries.Services.Actors.Controllers
{
    [Route("services/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private IActorsRepository _actors;
        public ActorsController(IActorsRepository actors)
        {
            _actors = actors;
        }
        
        [HttpGet(Name ="GetAll")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            var actors = await _actors.GetAll();
            if (actors is null) return NotFound();
            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActorId(int id)
        {
            var actor = await _actors.GetActorId(id);
            if (actor is null) return NotFound();
            return Ok(actor);
        }

        [HttpPost]
        public async Task<ActionResult> AddActor([FromBody] Actor actor)
        {
            if (await _actors.AddActor(actor)) return Ok();
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Actor actor)
        {
            actor.ActorId = id; 
            if (await _actors.UpdateActor(actor)) return Ok();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _actors.DeleteActor(id)) return NoContent();
            return NotFound();
        }
    }
}
