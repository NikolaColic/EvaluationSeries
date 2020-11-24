using EvaluationSeries.Services.Series.Context;
using EvaluationSeries.Services.Series.Entities;
using EvaluationSeries.Services.Series.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Repository
{
    public class ActorRepository : IActorRepository
    {
        private readonly SeriesDbContext _db;
        public ActorRepository(SeriesDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddActor(Actor a)
        {
            try
            {
                await _db.AddAsync(a);
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

                var actor = await GetActorById(id);
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

        public async Task<Actor> GetActorById(int id)
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

        public async Task<IEnumerable<Actor>> GetAllActors()
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

        public async Task<bool> UpdateActor(ActorCreate oldActor,Actor a)
        {
            try
            {

                var actorUpdate = await GetActorByName(oldActor.Name, oldActor.Surname);
                if (actorUpdate is null) return false;
                a.ActorId = actorUpdate.ActorId;
                _db.Entry(actorUpdate).CurrentValues.SetValues(a);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false; 
            }
        }
        private async Task<Actor> GetActorByName(string name, string surname)
        {
            try
            {
                var actor = await _db.Actor.SingleOrDefaultAsync((a) => a.Name == name && a.Surname == surname);
                return actor;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
