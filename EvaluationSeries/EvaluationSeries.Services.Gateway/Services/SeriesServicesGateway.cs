using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Help;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Services
{
    public class SeriesServicesGateway : ISeriesServicesGateway
    {
        private SeriesGrpc.SeriesGrpcClient _series;
        
        private IMapper _mapper;
        public SeriesServicesGateway(IMapper mapper)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5000");
            this._series = new SeriesGrpc.SeriesGrpcClient(channel);
            _mapper = mapper;
        }

        public async Task<bool> AddActor(Actor actor)
        {
            try
            {
                ActorAddSeries actorAdd = _mapper.Map<Actor, ActorAddSeries>(actor);
                var response = await _series.PostActorSeriesAsync(actorAdd);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> AddRole(Role role)
        {
            try
            {
                RoleAdd roleAdd = _mapper.Map<Role, RoleAdd>(role);
                var response = await _series.PostRoleAsync(roleAdd);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<bool> AddSeries(Series s)
        {
            try
            {
                var seriesFull = _mapper.Map<Series, SeriesFull>(s);
                var response = await _series.PostSeriesAsync(seriesFull);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteActor(int actorId)
        {
            try
            {
                var response = await _series.DeleteActorSeriesAsync(new SeriesId() { Id = actorId });
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> DeleteRole(int seriesId, int roleId)
        {
            try
            {
                var response = await _series.DeleteRoleAsync(new SeriesRoleId() { RoleId = roleId, IdSeries = seriesId });
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSeries(int id)
        {
            try
            {
                var response = await _series.DeleteSeriesAsync(new SeriesId() { Id = id});
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            try
            {
                var response = await _series.GetAllActorsAsync(new SeriesEmpty());
                if (response is null) return null;
                List<Actor> actors = new List<Actor>();
                response.Actors.ToList().ForEach((act) =>
                {
                    var actor = _mapper.Map<ActorAddSeries, Actor>(act);
                    actors.Add(actor);
                });
                return actors;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Series>> GetAllSeries()
        {
            try
            {
                var response = await _series.GetAllSeriesAsync(new SeriesEmpty());
                
                if (response is null) return null;
                List<Series> series = new List<Series>();
                response.Series.ToList().ForEach((ser) =>
                {
                    var seriesOne = _mapper.Map<SeriesFull, Series>(ser);
                    series.Add(seriesOne);
                });
                return series;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Role>> GetRoles(int id)
        {
            try
            {
                var response = await _series.GerRolesSeriesAsync(new SeriesId() { Id = id });
                if (response is null) return null;
                List<Role> roles = new List<Role>();
                response.Roles.ToList().ForEach((r) =>
                {
                    var role = _mapper.Map<RoleAdd, Role>(r);
                    roles.Add(role);
                });
                return roles;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Series> GetSeriesById(int id)
        {
            try
            {
                var response = await _series.GetSeriesByIdAsync(new SeriesId() { Id = id });
                if (response is null) return null;
                var series = _mapper.Map<SeriesFull, Series>(response.Series);
                return series;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateActor(Actor actor)
        {
            try
            {
                var actorAdd = _mapper.Map<Actor, ActorAddSeries>(actor);
                var response = await _series.PutActorSeriesAsync(actorAdd);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateSeries(Series s)
        {
            try
            {
                var seriesFull = _mapper.Map<Series, SeriesFull>(s);
                var response = await _series.PutSeriesAsync(seriesFull);
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
