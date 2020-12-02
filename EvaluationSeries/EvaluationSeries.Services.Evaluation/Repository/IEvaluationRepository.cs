using EvaluationSeries.Services.Evaluation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Evaluation.Repository
{
    public interface IEvaluationRepository
    {
        //Series 
        Task<bool> PostSeries(Series s);
        Task<bool> PutSeries(Series s);
        Task<bool> DeleteSeries(Series s);
        //User
        Task<bool> PostUser(User u);
        Task<bool> PutUser(User u);
        Task<bool> DeleteUser(User u);
        //Evaluation
        Task<IEnumerable<Evaluation2>> GetAllEvalutions();
        Task<Evaluation2> GetEvaluationById(int id);
        Task<bool> PostEvaluation(Evaluation2 evaluation);
        Task<bool> DeleteEvaluation(int id);
        //EvaluationCriterion 
        Task<IEnumerable<EvaluationCriterion>> GetAllCriterions();
        //Mark 
        Task<IEnumerable<Mark>> GetAllMarks();
        Task<bool> PostMarks(List<Mark> marks);
        Task<bool> PutMarks(List<Mark> marks);
        Task<bool> DeleteMarks(int evaluationId);




    }
}
