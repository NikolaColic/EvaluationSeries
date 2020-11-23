using EvaluationSeries.Services.Actors.Context;
using EvaluationSeries.Services.Actors.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Actors.Repository
{
    public class ActorsRepository : IActorsRepository
    {
        private readonly ActorsDbContext _db;

        public ActorsRepository(ActorsDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddActor(Actor actor)
        {
            try
            {
                await _db.Actor.AddAsync(actor);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false; 
            }
        }

        public async Task<bool> DeleteActor(int id)
        {
            try
            {
                var actor = await GetActorId(id);
                if (actor is null) return false;
                _db.Entry(actor).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false; 
            }

        }

        public async Task<Actor> GetActorId(int id)
        {
            try
            {
                var actor = await _db.Actor.SingleOrDefaultAsync((a) => a.ActorId == id);
                return actor;
            }
            catch (Exception)
            {
                return null;
            }
                
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
            try
            {
                var actors = await _db.Actor.ToListAsync();
                return actors;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateActor(Actor actor)
        {
            try
            {
                var actorUpdate = await GetActorId(actor.ActorId);
                if (actor is null) return false;
                _db.Entry(actorUpdate).CurrentValues.SetValues(actor);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
