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
            foreach(var actor in actors)
            {
                ActorAdd act = new ActorAdd()
                {
                    ActorId = actor.ActorId,
                    Biography = actor.Biography,
                    ImageUrl = actor.ImageUrl,
                    Name = actor.Name,
                    Surname = actor.Surname,
                    WikiUrl = actor.WikiUrl
                };
                actorsAdd.Add(act);
            }
            
            response.Actors.AddRange(actorsAdd);
            return response;
        }

        public override async Task<GetActorByIdResponse> GetActorsId(ActorId actorId, ServerCallContext context)
        {
            var actor = await _actor.GetActorId(actorId.Id);
            GetActorByIdResponse response = new GetActorByIdResponse();
            ActorAdd a = new ActorAdd()
            {
                ActorId = actor.ActorId,
                Biography = actor.Biography,
                ImageUrl = actor.ImageUrl,
                Name = actor.Name,
                Surname = actor.Surname,
                WikiUrl = actor.WikiUrl
            };
            response.Actor = a;
            return response;
        }
    }
}
