using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Evaluation.Entities;
using EvaluationSeries.Services.Evaluation.Repository;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Evaluation.Services
{
    public class EvaluationServices : EvaluationGrpc.EvaluationGrpcBase
    {
        private IEvaluationRepository _evaluation;
        private IMapper _mapper;
        private readonly ILogger<EvaluationServices> _logger;

        public EvaluationServices(IEvaluationRepository evaluation, IMapper mapper, ILogger<EvaluationServices> logger)
        {
            _evaluation = evaluation;
            _mapper = mapper;
            this._logger = logger;
        }
        public async override Task<EvaluationMessageResponse> DeleteEvaluation(EvaluationAddId request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.DeleteEvaluation(request.Id);
                if (!response) throw new Exception("Ne moze da obrise evaluaciju");
                return new EvaluationMessageResponse() { Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> DeleteMarks(EvaluationAddId request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.DeleteMarks(request.Id);
                if (response) throw new Exception("Ne moze ocene da obrise");
                return new EvaluationMessageResponse() { Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> DeleteSeries(SeriesEvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var series = _mapper.Map<SeriesEvaluationAdd, Series>(request);
                var response = await _evaluation.DeleteSeries(series);
                if (response) throw new Exception("Ne moze serija da obrise");
                return new EvaluationMessageResponse() { Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> DeleteUser(UserEvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var user = _mapper.Map<UserEvaluationAdd, User>(request);
                var response = await _evaluation.DeleteUser(user);
                if (response) throw new Exception("Ne moze korisnika da obrise");
                return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> PostEvaluation(EvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var evaluation = _mapper.Map<EvaluationAdd, Evaluation2>(request);
                var response = await _evaluation.PostEvaluation(evaluation);
                if (response) throw new Exception("EvaluationRepository - PostEvaluation");
                return new EvaluationMessageResponse() { Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> PostSeries(SeriesEvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var series = _mapper.Map<SeriesEvaluationAdd, Series>(request);
                var response = await _evaluation.PostSeries(series);
                if (response) throw new Exception("EvaluationRepository - PostSeries");
                return new EvaluationMessageResponse() { Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> PostMarks(MarksResponse request, ServerCallContext context)
        {
            try
            {
                List<Mark> marks = new List<Mark>(); 
                foreach(var markAdd in request.Marks)
                {
                    var mark = _mapper.Map<MarkAdd, Mark>(markAdd);
                    marks.Add(mark);
                }
                var response = await _evaluation.PostMarks(marks);
                if (response) throw new Exception("EvaluationRep - PostMarks");
                return new EvaluationMessageResponse() { Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> PostUser(UserEvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var user = _mapper.Map<UserEvaluationAdd, User>(request);
                var response = await _evaluation.PostUser(user);
                if (response) throw new Exception("EvaluationRep - PostUser");
                return new EvaluationMessageResponse() { Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }

        public override async Task<EvaluationMessageResponse> PutSeries(SeriesEvaluationUpdate request, ServerCallContext context)
        {
            try
            {
                var series = _mapper.Map<SeriesEvaluationAdd, Series>(request.SeriesAdd);
                var seriesUpdate = _mapper.Map<SeriesEvaluationAdd, Series>(request.SeriesUpdate);
                var response = await _evaluation.PutSeries(series, seriesUpdate);
                if (response) throw new Exception("EvaluationRep - PutSeries");
                return new EvaluationMessageResponse() { Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> PutMarks(MarksResponse request, ServerCallContext context)
        {
            try
            {
                List<Mark> marks = new List<Mark>();
                foreach (var markAdd in request.Marks)
                {
                    var mark = _mapper.Map<MarkAdd, Mark>(markAdd);
                    marks.Add(mark);
                }
                var response = await _evaluation.PutMarks(marks);
                if (response) throw new Exception("EvaluationRep - PutMarks");
                return new EvaluationMessageResponse() { Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> PutUser(UserEvaluationUpdate request, ServerCallContext context)
        {
            try
            {
                var userAdd = _mapper.Map<UserEvaluationAdd, User>(request.UserAdd);
                var userUpdate = _mapper.Map<UserEvaluationAdd, User>(request.UserUpdate);

                var response = await _evaluation.PutUser(userAdd, userUpdate);
                if (response) throw new Exception("EvaluationRep - PutUser");
                return new EvaluationMessageResponse() { Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationsResponse> GetAllEvaluations(EvaluationEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.GetAllEvalutions();
                if (response is null || response.Count() == 0) throw new Exception("EvaluationR - GetEvaluations");
                var evaluationsAdd = new List<EvaluationAdd>();
                response.ToList().ForEach((evaluation) =>
                {
                    var evaluationAdd = _mapper.Map<Evaluation2, EvaluationAdd>(evaluation);
                    evaluationsAdd.Add(evaluationAdd);
                });
                return new EvaluationsResponse() { Evaluations = { evaluationsAdd } };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new EvaluationsResponse() { };
            }
        }
        public override async Task<CriterionsResponse> GetAllCriteria(EvaluationEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.GetAllCriterions();
                if (response is null || response.Count() ==0) throw new Exception("EvaluationR - GetCriteria");
                var criterions = new List<CriterionAdd>();
                response.ToList().ForEach((criterion) =>
                {
                    var criterionAdd = _mapper.Map<EvaluationCriterion, CriterionAdd>(criterion);
                    criterions.Add(criterionAdd);
                });
                return new CriterionsResponse() { Criterions = { criterions } };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new CriterionsResponse() {};
            }
        }
        public override async Task<MarksResponse> GetAllMarks(EvaluationEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.GetAllMarks();
                if (response is null || response.Count() == 0) throw new Exception("EvaluationR - GetMarks");
                var marks = new List<MarkAdd>();
                response.ToList().ForEach((mark) =>
                {
                    var markAdd = _mapper.Map<Mark, MarkAdd>(mark);
                    marks.Add(markAdd);
                });
                return new MarksResponse() { Marks = { marks } };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new MarksResponse() {};
            }
        }
        public override async Task<EvaluationAdd> GetEvaluationById(EvaluationAddId request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.GetEvaluationById(request.Id);
                EvaluationAdd evaluatioAdd = null;
                if (response is null) throw new Exception("EvaluationR - GetEvaluId");
                var evaluation = _mapper.Map<Evaluation2, EvaluationAdd>(response);
                return evaluatioAdd;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                EvaluationAdd evaluatioAdd = null;
                return evaluatioAdd;
            }
        }


    }
}
