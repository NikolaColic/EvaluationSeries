using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Actors.Entities;
using EvaluationSeries.Services.Actors.Help;
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
            try
            {
                var actors = await _actor.GetAll();
                List<ActorAdd> actorsAdd = new List<ActorAdd>();
                foreach (var actor in actors)
                {
                    ActorAdd act = ConvertObject.Instance.CreateActorAdd(actor);
                    actorsAdd.Add(act);
                }
                return new GetActorsResponse() { Actors = { actorsAdd } };
            }
            catch (Exception)
            {
                List<ActorAdd> actorsException = null;
                return new GetActorsResponse() { Actors = { actorsException } };

            }
        }

        public override async Task<GetActorByIdResponse> GetActorsId(ActorId actorId, ServerCallContext context)
        {
            try
            {
                var actor = await _actor.GetActorId(actorId.Id);
                ActorAdd a = ConvertObject.Instance.CreateActorAdd(actor);
                return new GetActorByIdResponse() { Actor = a };
            }
            catch (Exception)
            {
                return new GetActorByIdResponse() { Actor = null };
            }
        }
        public override async Task<ActorsMessageResponse> PostActor(ActorAdd request, ServerCallContext context)
        {

            try
            {
                var response = await _actor.AddActor(ConvertObject.Instance.CreateActor(request));
                return response ? new ActorsMessageResponse() { Poruka = "Uspesno dodato", Signal = true } :
                    new ActorsMessageResponse() { Poruka = "Neuspesno dodato", Signal = false };
            }
            catch (Exception)
            {
                return new ActorsMessageResponse() { Poruka = "Doslo je do greske", Signal = false };
            }

        }
        public override async Task<ActorsMessageResponse> PutActor(ActorAdd request, ServerCallContext context)
        {
            try
            {
                var response = await _actor.UpdateActor(ConvertObject.Instance.CreateActor(request));
                return response ? new ActorsMessageResponse() { Poruka = "Uspesno izmenjeno", Signal = true } :
                    new ActorsMessageResponse() { Poruka = "Neuspesno izmenjeno", Signal = false };
            }
            catch (Exception)
            {
                return new ActorsMessageResponse() { Poruka = "Doslo je do greske", Signal = false };
            }
        }
        public override async Task<ActorsMessageResponse> DeleteActor(ActorId request, ServerCallContext context)
        {
            try
            {
                var response = await _actor.DeleteActor(request.Id);
                return response ? new ActorsMessageResponse() { Poruka = "Uspesno obrisano", Signal = true } :
                    new ActorsMessageResponse() { Poruka = "Neuspesno obrisano", Signal = false };
            }
            catch (Exception)
            {
                return new ActorsMessageResponse() { Poruka = "Doslo je do greske", Signal = false };
            }
        }
    }
}
