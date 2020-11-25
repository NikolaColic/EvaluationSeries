using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Actors.Entities;
using EvaluationSeries.Services.Actors.Repository;
using Grpc.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Actors.Services
{
    public class ActorServices : ActorsGrpc.ActorsGrpcBase
    {
        private IActorsRepository _actor;
        public ActorServices(IActorsRepository actor)
        {
            _actor = actor;
        }
        public override async Task<GetActorsResponse> GetActors(ActorEmpty a, ServerCallContext context)
        {
            var actors = await _actor.GetAll();
            GetActorsResponse response = new GetActorsResponse();
            List<ActorAdd> actorsAdd = new List<ActorAdd>();

            
            response.Actors.Add((List<ActorAdd>)actors);
            return response;
        }
    }
}
