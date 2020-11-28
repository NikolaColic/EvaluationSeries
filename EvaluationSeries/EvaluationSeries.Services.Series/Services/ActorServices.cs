using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Series.Help;
using EvaluationSeries.Services.Series.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Services
{
    public class ActorServices : IActorServices
    {
        private ActorsGrpc.ActorsGrpcClient _actorService;

        public ActorServices(ActorsGrpc.ActorsGrpcClient actorService)
        {
            _actorService = actorService;
        }


        public async Task<bool> DeleteActor(int id)
        {
            try
            {
                ActorsMessageResponse response = await _actorService.DeleteActorAsync(new ActorId() { Id = id });
                var signal = (bool)response.Signal;
                return signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ActorCreate> GetActorById(int id)
        {
            try
            {
                GetActorByIdResponse actor = await _actorService.GetActorsIdAsync(new ActorId() { Id = id });
                ActorCreate actorCreate = ConvertObject.Instance.CreateActorCreate(actor.Actor);          
                return actorCreate;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ActorCreate>> GetAll()
        {
            try
            {
                GetActorsResponse response = await _actorService.GetActorsAsync(new ActorEmpty());
                List<ActorCreate> listaActora = new List<ActorCreate>();
                response.Actors.ToList().ForEach(actor =>
                {
                    ActorCreate actorCreate = ConvertObject.Instance.CreateActorCreate(actor);
                    listaActora.Add(actorCreate);
                });
                return listaActora;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> PostActor(ActorCreate actor)
        {
            try
            {
                ActorAdd act = ConvertObject.Instance.CreateActorAdd(actor);
                ActorsMessageResponse response = await _actorService.PostActorAsync(act);
                var signal = (bool)response.Signal;
                return signal;

            }
            catch (Exception)
            {
                return false;
            }
            

        }

        public async Task<bool> PutActor(ActorCreate actor)
        {
            try
            {
                ActorAdd act = ConvertObject.Instance.CreateActorAdd(actor);
                ActorsMessageResponse response = await _actorService.PutActorAsync(act);
                var signal = (bool)response.Signal;
                return signal;

            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
