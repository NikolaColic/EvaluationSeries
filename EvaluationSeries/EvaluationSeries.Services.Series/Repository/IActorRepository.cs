using EvaluationSeries.Services.Series.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Repository
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetAllActors();
        Task<Actor> GetActorById(int id);
        Task<bool> AddActor(Actor a);
        Task<bool> UpdateActor(Actor oldActor,Actor a);
        Task<bool> DeleteActor(Actor oldActor);
    }
}
