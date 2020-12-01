using EvaluationSeries.Services.Gateway.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Services
{
    public interface ISeriesServicesGateway
    {
        Task<IEnumerable<Series>> GetAllSeries();
        Task<Series> GetSeriesById(int id);
        Task<bool> AddSeries(Series s); 
        Task<bool> UpdateSeries(Series s);
        Task<bool> DeleteSeries(int id);

        Task<IEnumerable<Role>> GetRoles(int id);
        Task<bool> AddRole(Role role);
        Task<bool> DeleteRole(int seriesId, int roleId);

        Task<IEnumerable<Actor>> GetActors();
        Task<bool> AddActor(Actor actor);
        Task<bool> UpdateActor(Actor actor);
        Task<bool> DeleteActor(int actorId);

        Task<IEnumerable<Country>> GetAllCountries();
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<IEnumerable<Role>> GetAllRoles();








    }
}
