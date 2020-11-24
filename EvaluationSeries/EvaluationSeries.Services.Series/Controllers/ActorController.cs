using EvaluationSeries.Services.Series.Entities;
using EvaluationSeries.Services.Series.Models;
using EvaluationSeries.Services.Series.Repository;
using EvaluationSeries.Services.Series.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationSeries.Services.Series.Controllers
{
    [Route("services/series/actor")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private IActorRepository _actor;
        private IActorServices _service;
        public ActorController(IActorRepository actor, IActorServices service)
        {
            _actor = actor;
            _service = service;
        }
        [HttpGet(Name = "GetAllActors")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAllActors()
        {
            var actors = await _service.GetAll();
            if (actors is null) return NotFound();
            return Ok(actors);
        }            

        [HttpGet("{id}")]
        public async Task<ActionResult<Series2>> GetSeriesById(int id)
        {
            var actor = await _actor.GetActorById(id);
            if (actor is null) return NotFound();
            return Ok(actor);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Series2>>> AddSeries([FromBody] ActorCreate actor)
        {
            if (!await _service.PostActor(actor)) return NotFound();

            if (await _actor.AddActor(CreateActor(actor))) return RedirectToAction("GetAllActors");
            return NotFound();
        }
        private Actor CreateActor(ActorCreate actor)
        {
            Actor a = new Actor()
            {
                Name = actor.Name,
                Surname = actor.Surname
            };
            return a;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Series2>>> UpdateSeries([FromBody] Actor actor)
        {
            if (await _actor.UpdateActor(actor)) return RedirectToAction("GetAllActors");
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _actor.DeleteActor(id)) return NoContent();
            return NotFound();
        }
    }
}
