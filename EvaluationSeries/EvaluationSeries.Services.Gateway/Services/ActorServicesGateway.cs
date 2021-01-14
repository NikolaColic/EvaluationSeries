using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Services
{
    public class ActorServicesGateway : IActorServicesGateway
    {
        private ActorsGrpc.ActorsGrpcClient _actor;

        private IMapper _mapper;
        private readonly ILogger<ActorServicesGateway> _logger;

        public ActorServicesGateway(IMapper mapper, ILogger<ActorServicesGateway> logger)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            this._actor = new ActorsGrpc.ActorsGrpcClient(channel);
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            try
            {
                var response = await _actor.GetActorsAsync(new ActorEmpty());
                if (response.Actors is null || response.Actors.Count == 0) return null;
                var actors = new List<Actor>();
                foreach (var el in response.Actors)
                {
                    var actor = _mapper.Map<ActorAdd, Actor>(el);
                    actors.Add(actor);
                }
                return actors;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }
        }

        public async Task<bool> AddActor(Actor actor)
        {
            try
            {
                var actorAdd = _mapper.Map<Actor, ActorAdd>(actor);
                var response = await _actor.PostActorAsync(actorAdd);
                return response.Signal;
            }
            catch (Exception e)
            {
                _logger.LogError(e,"ERROR");
                return false;
            }
        }

        public async Task<bool> UpdateActor(Actor actor)
        {
            try
            {
                var actorAdd = _mapper.Map<Actor, ActorAdd>(actor);
                var response = await _actor.PutActorAsync(actorAdd);
                return response.Signal;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return false;
            }
        }

        public async Task<bool> DeleteActor(int id)
        {
            try
            {
                var response = await _actor.DeleteActorAsync(new ActorId() { Id = id});
                return response.Signal;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return false;
            }
        }

        public async Task<Actor> GetActorById(int id)
        {
            try
            {
                var response = await _actor.GetActorsIdAsync(new ActorId() { Id = id });
                if (response is null) return null;
                var actor = _mapper.Map<ActorAdd, Actor>(response.Actor);
                return actor;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }
        }
    }
}
