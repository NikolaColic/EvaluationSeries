using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Services
{
    public class SeriesServicesGateway : ISeriesServicesGateway
    {
        private SeriesGrpc.SeriesGrpcClient _series;
        public SeriesServicesGateway(SeriesGrpc.SeriesGrpcClient series)
        {
            this._series = series;
        }

        public async Task<bool> AddActor(Actor actor)
        {
            try
            {
                ActorAddSeries actorAdd = ConvertObject.Instance.CreateActorAddSeries(actor);
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
                RoleAdd roleAdd = ConvertObject.Instance.CreateRoleAdd(role);
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
                var response = await _series.PostSeriesAsync(ConvertObject.Instance.CreateSeriesFull(s));
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
                    var actor = ConvertObject.Instance.CreateActor(act);
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
                    var seriesOne = ConvertObject.Instance.CreateSeries(ser);
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
                    var role = ConvertObject.Instance.CreateRole(r);
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
                var series = ConvertObject.Instance.CreateSeries(response.Series);
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
                ActorAddSeries actorAdd = ConvertObject.Instance.CreateActorAddSeries(actor);
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
               
                var response = await _series.PutSeriesAsync(ConvertObject.Instance.CreateSeriesFull(s));
                return response.Signal;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
