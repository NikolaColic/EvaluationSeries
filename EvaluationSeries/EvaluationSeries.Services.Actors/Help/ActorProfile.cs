using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Actors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Actors.Help
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<Actor, ActorAdd>();
            CreateMap<ActorAdd, Actor>();
        }
    }
}
