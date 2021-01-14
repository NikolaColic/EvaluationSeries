using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Actors.Entities;
using EvaluationSeries.Services.Actors.Help;
using EvaluationSeries.Services.Actors.Repository;
using Grpc.Core;
using Microsoft.Extensions.Logging;
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
        private IMapper _mapper;
        private readonly ILogger<ActorServices> _logger;

        public ActorServices(IActorsRepository actor, IMapper mapper, ILogger<ActorServices> logger)
        {
            _actor = actor;
            this._mapper = mapper;
            this._logger = logger;
        }
        public override async Task<GetActorsResponse> GetActors(ActorEmpty a, ServerCallContext context)
        {
            try
            {
                var actors = await _actor.GetAll();
                if (actors is null) throw new Exception("Neuspesno uzeti glumci!");
                List<ActorAdd> actorsAdd = new List<ActorAdd>();
                foreach (var actor in actors)
                {
                    ActorAdd act = _mapper.Map<Actor, ActorAdd>(actor);
                    actorsAdd.Add(act);
                }
                return new GetActorsResponse() { Actors = { actorsAdd } };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new GetActorsResponse() { };
            }
        }

        public override async Task<GetActorByIdResponse> GetActorsId(ActorId actorId, ServerCallContext context)
        {
            try
            {
                var actor = await _actor.GetActorId(actorId.Id);
                if (actor is null) throw new Exception("Ne moze pronaci glumca");
                ActorAdd act = _mapper.Map<Actor, ActorAdd>(actor);
                return new GetActorByIdResponse() { Actor = act };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new GetActorByIdResponse() { Actor = null };
            }
        }
        public override async Task<ActorsMessageResponse> PostActor(ActorAdd request, ServerCallContext context)
        {

            try
            {
                var actor = _mapper.Map<ActorAdd, Actor>(request);
                var response = await _actor.AddActor(actor);
                if (!response) throw new Exception("Ne moze da doda glumca");
                return new ActorsMessageResponse() { Poruka = "Uspesno dodato", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new ActorsMessageResponse() { Poruka = "Doslo je do greske", Signal = false };
            }

        }
        public override async Task<ActorsMessageResponse> PutActor(ActorAdd request, ServerCallContext context)
        {
            try
            {
                var actor = _mapper.Map<ActorAdd, Actor>(request);
                var response = await _actor.UpdateActor(actor);
                if (!response) throw new Exception("Ne moze da doda glumca");
                return new ActorsMessageResponse() { Poruka = "Uspesno izmenjeno", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new ActorsMessageResponse() { Poruka = "Doslo je do greske", Signal = false };
            }
        }
        public override async Task<ActorsMessageResponse> DeleteActor(ActorId request, ServerCallContext context)
        {
            try
            {
                var response = await _actor.DeleteActor(request.Id);
                if (!response) throw new Exception("Ne moze da obrise glumca");
                return new ActorsMessageResponse() { Poruka = "Uspesno obrisano", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new ActorsMessageResponse() { Poruka = "Doslo je do greske", Signal = false };
            }
        }
    }
}
