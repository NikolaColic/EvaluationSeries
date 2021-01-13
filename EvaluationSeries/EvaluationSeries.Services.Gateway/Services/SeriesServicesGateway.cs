using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Help;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SeriesServicesGateway> _logger;
        public SeriesServicesGateway(IMapper mapper, ILogger<SeriesServicesGateway> logger)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5000");
            this._series = new SeriesGrpc.SeriesGrpcClient(channel);
            _mapper = mapper;
            this._logger = logger;
        }

        public async Task<bool> AddActor(Actor actor)
        {
            try
            {
                ActorAddSeries actorAdd = _mapper.Map<Actor, ActorAddSeries>(actor);
                var response = await _series.PostActorSeriesAsync(actorAdd);
                return response.Signal;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
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
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
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
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
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
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
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
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
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
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return false;
            }
        }

        public async Task<IEnumerable<Actor>> GetActors()
        {
            try
            {
                var response = await _series.GetAllActorsAsync(new SeriesEmpty());
                if (response.Actors is null || response.Actors.Count == 0) return null;
                List<Actor> actors = new List<Actor>();
                response.Actors.ToList().ForEach((act) =>
                {
                    var actor = _mapper.Map<ActorAddSeries, Actor>(act);
                    actors.Add(actor);
                });
                return actors;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            try
            {
                var response = await _series.GetAllCountryAsync(new SeriesEmpty());

                if (response.Countries is null || response.Countries.Count == 0) return null;
                List<Country> countries = new List<Country>();
                response.Countries.ToList().ForEach((country) =>
                {
                    var countryOne = _mapper.Map<CountryFull, Country>(country);
                    countries.Add(countryOne);
                });
                return countries;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }

        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            try
            {
                var response = await _series.GetAllGenreAsync(new SeriesEmpty());

                if (response.Genres is null || response.Genres.Count == 0) return null;
                List<Genre> genres = new List<Genre>();
                response.Genres.ToList().ForEach((genre) =>
                {
                    var genreOne = _mapper.Map<GenreFull, Genre>(genre);
                    genres.Add(genreOne);
                });
                return genres;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            try
            {
                var response = await _series.GetAllRolesAsync(new SeriesEmpty());

                if (response.Roles is null || response.Roles.Count == 0) return null;
                List<Role> roles = new List<Role>();
                response.Roles.ToList().ForEach((role) =>
                {
                    var roleOne = _mapper.Map<RoleAdd, Role>(role);
                    roles.Add(roleOne);
                });
                return roles;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }
        }

        public async Task<IEnumerable<Series>> GetAllSeries()
        {
            try
            {
                var response = await _series.GetAllSeriesAsync(new SeriesEmpty());
                
                if (!response.Signal || response.Series.Count ==0) return null;
                List<Series> series = new List<Series>();
                response.Series.ToList().ForEach((ser) =>
                {
                    var seriesOne = _mapper.Map<SeriesFull, Series>(ser);
                    series.Add(seriesOne);
                });
                return series;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }
        }

        public async Task<IEnumerable<Role>> GetRoles(int id)
        {
            try
            {
                var response = await _series.GerRolesSeriesAsync(new SeriesId() { Id = id });
                if (response.Roles is null || response.Roles.Count == 0) return null;
                List<Role> roles = new List<Role>();
                response.Roles.ToList().ForEach((r) =>
                {
                    var role = _mapper.Map<RoleAdd, Role>(r);
                    roles.Add(role);
                });
                return roles;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }
        }

        public async Task<Series> GetSeriesById(int id)
        {
            try
            {
                var response = await _series.GetSeriesByIdAsync(new SeriesId() { Id = id });
                if (response.Series is null) return null;
                var series = _mapper.Map<SeriesFull, Series>(response.Series);
                return series;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
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
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
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
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return false;
            }
        }
    }
}
