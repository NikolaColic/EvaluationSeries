using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Evaluation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Evaluation.Help
{
    public class EvaluationProfile : Profile
    {
        public EvaluationProfile()
        {
            CreateMap<SeriesEvaluationAdd, Series>();
            CreateMap<Series, SeriesEvaluationAdd>();

            CreateMap<User, UserEvaluationAdd>();
            CreateMap<UserEvaluationAdd, User>();

            CreateMap<CriterionAdd, EvaluationCriterion>();
            CreateMap<EvaluationCriterion, CriterionAdd>();

            CreateMap<Evaluation2, EvaluationAdd>()
                .ForMember((dest) => dest.User,
                opt => opt.MapFrom((s) => s.User))
                .ForMember((dest) => dest.Series,
                opt => opt.MapFrom((s) => s.Series));
            CreateMap<EvaluationAdd, Evaluation2>()
                .ForMember((dest) => dest.User,
                opt => opt.MapFrom((s) => s.User))
                .ForMember((dest) => dest.Series,
                opt => opt.MapFrom((s) => s.Series));

            CreateMap<Mark, MarkAdd>()
               .ForMember((dest) => dest.Evaluation,
               opt => opt.MapFrom((s) => s.Evaluation))
               .ForMember((dest) => dest.Criterion,
               opt => opt.MapFrom((s) => s.Criterion));

            CreateMap<MarkAdd, Mark>()
               .ForMember((dest) => dest.Evaluation,
               opt => opt.MapFrom((s) => s.Evaluation))
               .ForMember((dest) => dest.Criterion,
               opt => opt.MapFrom((s) => s.Criterion));


        }
    }
}
