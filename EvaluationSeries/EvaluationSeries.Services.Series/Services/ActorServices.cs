using EvaluationSeries.Grpc;
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
        private readonly HttpClient client;
        private ActorsGrpc.ActorsGrpcClient _actorService;

        public ActorServices(ActorsGrpc.ActorsGrpcClient actorService)
        {
            _actorService = actorService;
            //this.client = client;
        }

        public async Task<bool> DeleteActor(int id)
        {
            try
            {
                var response = await client.DeleteAsync($"services/actors/{id}");
                if (!response.IsSuccessStatusCode) return false;
                return true;
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
                //var response = await client.GetAsync($"services/actors/{id}");
                //if (!response.IsSuccessStatusCode) return null;
                //var dataAsString = await response.Content.ReadAsStringAsync();
                //var actors = JsonConvert.DeserializeObject<ActorCreate>(dataAsString);
                //return actors;
                GetActorByIdResponse actor = await _actorService.GetActorsIdAsync(new ActorId() { Id = id });
                var nik = 5;
                return null;

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
                //var response = await client.GetAsync($"services/actors");
                //if (!response.IsSuccessStatusCode) return null;
                //var dataAsString = await response.Content.ReadAsStringAsync();
                //var actors = JsonConvert.DeserializeObject<List<ActorCreate>>(dataAsString);
                //return actors;
                GetActorsResponse response = await _actorService.GetActorsAsync(new ActorEmpty());
                var nik = 5;
                return null;
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
                var data = new System.Net.Http.StringContent(JsonConvert.SerializeObject(actor), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"services/actors", data);
                if (response.IsSuccessStatusCode) return true;
                return false;
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
                var data = new System.Net.Http.StringContent(JsonConvert.SerializeObject(actor), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"services/actors/{actor.ActorId}", data);
                if (response.IsSuccessStatusCode) return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
