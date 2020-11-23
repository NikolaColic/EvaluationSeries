using EvaluationSeries.Services.Actors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Actors.Repository
{
    public interface IActorsRepository
    {
        Task<IEnumerable<Actor>> GetAll();
        Task<Actor> GetActorId(int id);
        Task<bool> AddActor(Actor actor); 
        Task<bool> UpdateActor(Actor actor);
        Task<bool> DeleteActor(int id);

    }
}
