using EvaluationSeries.Services.Series.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Repository
{
    public interface ISeriesRepository
    {
        //Actor 
        //Task<IEnumerable<Actor>> GetAllActors();
        //Task<Actor> GellActorById(int id);
        //Task<bool> AddActor(Actor a);
        //Task<bool> UpdateActor(Actor a);
        //Task<bool> DeleteActor(int i);
        //Series 
        Task<IEnumerable<Series2>> GetAllSeries();
        Task<Series2> GetSeriesById(int id);
        Task<bool> AddSeries(Series2 s);
        Task<bool> UpdateSeries(Series2 s);
        Task<bool> DeleteSeries(int i);
        //Role
        Task<IEnumerable<Role>> GetRolesSeries(int id);
        Task<bool> AddRole(int id, Role role);
        Task<bool> DeleteRole(int seriesId, int roleId); 






    }
}
