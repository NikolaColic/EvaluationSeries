using EvaluationSeries.Services.Gateway.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Services
{
    public interface IActorServicesGateway
    {
        Task<IEnumerable<Actor>> GetActors();
        Task<bool> AddActor(Actor actor);
        Task<bool> UpdateActor(Actor actor);
        Task<bool> DeleteActor(int id);
        Task<Actor> GetActorById(int id);
    }
}
