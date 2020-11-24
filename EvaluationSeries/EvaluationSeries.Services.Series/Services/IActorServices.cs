using EvaluationSeries.Services.Series.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Services
{
    public interface IActorServices
    {
        Task<IEnumerable<ActorCreate>> GetAll();
        Task<ActorCreate> GetActorById(int id); 
        Task<bool> PostActor(ActorCreate actor);
        Task<bool> PutActor(ActorCreate actor);
        Task<bool> DeleteActor(int id);

    }
}
