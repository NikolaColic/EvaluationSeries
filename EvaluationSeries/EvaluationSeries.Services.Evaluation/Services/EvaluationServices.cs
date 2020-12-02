using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Evaluation.Entities;
using EvaluationSeries.Services.Evaluation.Repository;
using Grpc.Core;
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
        public EvaluationServices(IEvaluationRepository evaluation, IMapper mapper)
        {
            _evaluation = evaluation;
            _mapper = mapper;
        }
        public async override Task<EvaluationMessageResponse> DeleteEvaluation(EvaluationAddId request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.DeleteEvaluation(request.Id);
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> DeleteMarks(EvaluationAddId request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.DeleteMarks(request.Id);
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> DeleteSeries(SeriesEvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var series = _mapper.Map<SeriesEvaluationAdd, Series>(request);
                var response = await _evaluation.DeleteSeries(series);
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> DeleteUser(UserEvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var user = _mapper.Map<UserEvaluationAdd, User>(request);
                var response = await _evaluation.DeleteUser(user);
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> PostEvaluation(EvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var evaluation = _mapper.Map<EvaluationAdd, Evaluation2>(request);
                var response = await _evaluation.PostEvaluation(evaluation);
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> PostSeries(SeriesEvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var series = _mapper.Map<SeriesEvaluationAdd, Series>(request);
                var response = await _evaluation.PostSeries(series);
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
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
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> PostUser(UserEvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var user = _mapper.Map<UserEvaluationAdd, User>(request);
                var response = await _evaluation.PostUser(user);
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
                return new EvaluationMessageResponse() { Signal = false };
            }
        }

        public override async Task<EvaluationMessageResponse> PutSeries(SeriesEvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var series = _mapper.Map<SeriesEvaluationAdd, Series>(request);
                var response = await _evaluation.PutSeries(series);
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
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
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationMessageResponse> PutUser(UserEvaluationAdd request, ServerCallContext context)
        {
            try
            {
                var user = _mapper.Map<UserEvaluationAdd, User>(request);
                var response = await _evaluation.PutUser(user);
                if (response) return new EvaluationMessageResponse() { Signal = true };
                return new EvaluationMessageResponse() { Signal = false };
            }
            catch (Exception)
            {
                return new EvaluationMessageResponse() { Signal = false };
            }
        }
        public override async Task<EvaluationsResponse> GetAllEvaluations(EvaluationEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.GetAllEvalutions();
                List<EvaluationAdd> evaluationsAdd = null;
                if (response is null) return new EvaluationsResponse() { Evaluations = { evaluationsAdd } };
                evaluationsAdd = new List<EvaluationAdd>();
                response.ToList().ForEach((evaluation) =>
                {
                    var evaluationAdd = _mapper.Map<Evaluation2, EvaluationAdd>(evaluation);
                    evaluationsAdd.Add(evaluationAdd);
                });
                return new EvaluationsResponse() { Evaluations = { evaluationsAdd } };
            }
            catch (Exception)
            {
                List<EvaluationAdd> evaluationsAdd = null;
                return new EvaluationsResponse()
                {
                    Evaluations = { evaluationsAdd }
                };
            }
        }
        public override async Task<CriterionsResponse> GetAllCriteria(EvaluationEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.GetAllCriterions();
                List<CriterionAdd> criterions = null;
                if (response is null) return new CriterionsResponse() { Criterions = { criterions } };
                criterions = new List<CriterionAdd>();
                response.ToList().ForEach((criterion) =>
                {
                    var criterionAdd = _mapper.Map<EvaluationCriterion, CriterionAdd>(criterion);
                    criterions.Add(criterionAdd);
                });
                return new CriterionsResponse() { Criterions = { criterions } };
            }
            catch (Exception)
            {
                List<CriterionAdd> criterions = null;
                return new CriterionsResponse() { Criterions = { criterions } };
            }
        }
        public override async Task<MarksResponse> GetAllMarks(EvaluationEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.GetAllMarks();
                List<MarkAdd> marks = null;
                if (response is null) return new MarksResponse() { Marks = { marks } };
                marks = new List<MarkAdd>();
                response.ToList().ForEach((mark) =>
                {
                    var markAdd = _mapper.Map<Mark, MarkAdd>(mark);
                    marks.Add(markAdd);
                });
                return new MarksResponse() { Marks = { marks } };
            }
            catch (Exception)
            {
                List<MarkAdd> marks = null;
                return new MarksResponse() { Marks = { marks } };
            }
        }
        public override async Task<EvaluationAdd> GetEvaluationById(EvaluationAddId request, ServerCallContext context)
        {
            try
            {
                var response = await _evaluation.GetEvaluationById(request.Id);
                EvaluationAdd evaluatioAdd = null;
                if (response is null) return evaluatioAdd;
                var evaluation = _mapper.Map<Evaluation2, EvaluationAdd>(response);
                return evaluation;
            }
            catch (Exception)
            {
                EvaluationAdd evaluatioAdd = null;
                return evaluatioAdd;
            }
        }


    }
}
