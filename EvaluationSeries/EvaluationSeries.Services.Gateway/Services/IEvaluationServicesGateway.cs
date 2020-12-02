using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Services
{
    public interface IEvaluationServicesGateway
    {
        Task<bool> AddSeries(Series series);
        Task<bool> UpdateSeries(Series series, Series update);
        Task<bool> DeleteSeries(Series series);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user, User userUpdate);
        Task<bool> DeleteUser(User user);
        Task<IEnumerable<Evaluation>> GetAllEvaluations();
        Task<Evaluation> GetEvaluationById(int id);
        Task<bool> AddEvaluation(EvaluationCreate evaluation);
        Task<bool> DeleteEvaluation(int id);
        Task<IEnumerable<EvaluationCriterion>> GetAllCriterions();
        Task<IEnumerable<Mark>> GetAllMarks();

        Task<bool> UpdateMarks(List<Mark> marks);
        Task<bool> DeleteMarks(int evaluationId);






    }
}
