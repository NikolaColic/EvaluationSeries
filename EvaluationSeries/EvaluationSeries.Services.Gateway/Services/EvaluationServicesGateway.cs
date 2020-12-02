using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Models;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Services
{
    public class EvaluationServicesGateway : IEvaluationServicesGateway
    {
        private EvaluationGrpc.EvaluationGrpcClient _evaluation;

        private IMapper _mapper;
        public EvaluationServicesGateway(IMapper mapper)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5003");
            this._evaluation = new EvaluationGrpc.EvaluationGrpcClient(channel);
            _mapper = mapper;
        }
        public async Task<bool> AddEvaluation(EvaluationCreate evaluationCreate)
        {
            var evaluation = _mapper.Map<EvaluationCreate, Evaluation>(evaluationCreate);
            var evaluationAdd = _mapper.Map<Evaluation, EvaluationAdd>(evaluation);
            var response = await _evaluation.PostEvaluationAsync(evaluationAdd);
            if (!response.Signal) return false;

            return true;
            //ovo zavrsiti 
         }

        public async Task<bool> AddSeries(Series series)
        {
            try
            {
                var seriesAdd = _mapper.Map<Series, SeriesEvaluationAdd>(series);
                var response = await _evaluation.PostSeriesAsync(seriesAdd);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                var userAdd = _mapper.Map<User, UserEvaluationAdd>(user);
                var response = await _evaluation.PostUserAsync(userAdd);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEvaluation(int id)
        {
            try
            {
                var response = await _evaluation.DeleteEvaluationAsync(new EvaluationAddId() { Id = id });
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMarks(int evaluationId)
        {
            try
            {
                var response = await _evaluation.DeleteMarksAsync(new EvaluationAddId() { Id = evaluationId });
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSeries(Series series)
        {
            try
            {
                var seriesAdd = _mapper.Map<Series, SeriesEvaluationAdd>(series);
                var response = await _evaluation.DeleteSeriesAsync(seriesAdd);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(User user)
        {
            try
            {
                var userAdd = _mapper.Map<User, UserEvaluationAdd>(user);
                var response = await _evaluation.DeleteUserAsync(userAdd);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<EvaluationCriterion>> GetAllCriterions()
        {
            try
            {
                var response = await _evaluation.GetAllCriteriaAsync(new EvaluationEmpty());
                if (response is null) return null;
                List<EvaluationCriterion> criterions = new List<EvaluationCriterion>();
                response.Criterions.ToList().ForEach((criterion) =>
                {
                    var cr = _mapper.Map<CriterionAdd, EvaluationCriterion>(criterion);
                    criterions.Add(cr);
                });
                return criterions;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Evaluation>> GetAllEvaluations()
        {
            try
            {
                var response = await _evaluation.GetAllEvaluationsAsync(new EvaluationEmpty());
                if (response is null) return null;
                List<Evaluation> evaluations = new List<Evaluation>();
                response.Evaluations.ToList().ForEach((evaluation) =>
                {
                    var cr = _mapper.Map<EvaluationAdd, Evaluation>(evaluation);
                    evaluations.Add(cr);
                });
                return evaluations;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Mark>> GetAllMarks()
        {
            try
            {
                var response = await _evaluation.GetAllMarksAsync(new EvaluationEmpty());
                if (response is null) return null;
                List<Mark> marks = new List<Mark>();
                response.Marks.ToList().ForEach((mark) =>
                {
                    var cr = _mapper.Map<MarkAdd, Mark>(mark);
                    marks.Add(cr);
                });
                return marks;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Evaluation> GetEvaluationById(int id)
        {
            try
            {
                var response = await _evaluation.GetEvaluationByIdAsync(new EvaluationAddId() { Id = id});
                if (response is null) return null;
                var evaluation = _mapper.Map<EvaluationAdd, Evaluation>(response);
                return evaluation;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateMarks(List<Mark> marks)
        {
            try
            {
                List<MarkAdd> marksAdd = new List<MarkAdd>();
                marks.ForEach((mark) =>
                {
                    var markAdd = _mapper.Map<Mark, MarkAdd>(mark);
                    marksAdd.Add(markAdd);
                });
                var response = await _evaluation.PutMarksAsync(new MarksResponse() { Marks = { marksAdd } });
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateSeries(Series series)
        {
            try
            {
                var seriesAdd = _mapper.Map<Series, SeriesEvaluationAdd>(series);
                var response = await _evaluation.PutSeriesAsync(seriesAdd);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                var userAdd = _mapper.Map<User, UserEvaluationAdd>(user);
                var response = await _evaluation.PutUserAsync(userAdd);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
