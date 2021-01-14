using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Series.Entities;
using EvaluationSeries.Services.Series.Help;
using EvaluationSeries.Services.Series.Repository;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Services
{
    public class SeriesServices : SeriesGrpc.SeriesGrpcBase
    {
        private ISeriesRepository _series;
        private readonly ILogger<SeriesServices> _logger;
        private IActorRepository _actorRepository;
        private IMapper _mapper;

        public SeriesServices(ISeriesRepository series, IActorRepository actorRepository,
            IMapper mapper, ILogger<SeriesServices> logger)
        {
            this._actorRepository = actorRepository;
            this._series = series;
            this._mapper = mapper;
            this._logger = logger;
        }
        public override async Task<GetSeriesResponse> GetAllSeries(SeriesEmpty request, ServerCallContext context)
        {
            try
            {
                var series = await _series.GetAllSeries();
                if (series is null || series.Count() == 0) throw new Exception("SeriesRep - GetSeries");
                List<SeriesFull> seriesFull = new List<SeriesFull>();
                series.ToList().ForEach((ser) =>
                {
                    var sFull = _mapper.Map<Series2, SeriesFull>(ser);
                    seriesFull.Add(sFull);
                });
                return new GetSeriesResponse() { Series = { seriesFull }, Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new GetSeriesResponse() { Signal = false };
            }

        }
        public override async Task<GetSeriesByIdResponse> GetSeriesById(SeriesId request, ServerCallContext context)
        {
            try
            {
                var series = await _series.GetSeriesById(request.Id);
                if (series is null) throw new Exception("SeriesRep - GetSeriesById");
                var seriesFull = _mapper.Map<Series2, SeriesFull>(series);
                return new GetSeriesByIdResponse() { Series = seriesFull, Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new GetSeriesByIdResponse() { Series = null, Signal = false };
            }

        }

        public override async Task<SeriesMessageResponse> PostSeries(SeriesFull request, ServerCallContext context)
        {
            try
            {
                var series = _mapper.Map<SeriesFull, Series2>(request);
                var response = await _series.AddSeries(series);
                if (!response) throw new Exception("SeriesRep - PostSeries");
                return new SeriesMessageResponse() { Signal = true, Poruka = "Uspesno sacuvano" };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new SeriesMessageResponse() { Signal = false, Poruka = "Greska" };
            }
        }
        public override async Task<SeriesMessageResponse> PutSeries(SeriesFull request, ServerCallContext context)
        {
            try
            {
                var series = _mapper.Map<SeriesFull, Series2>(request);
                var response = await _series.UpdateSeries(series);
                if (!response) throw new Exception("SeriesRep - PostSeries");
                return new SeriesMessageResponse() { Signal = true, Poruka = "Uspesno izmenjeno" };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new SeriesMessageResponse() { Signal = false, Poruka = "Greska" };
            }
        }
        public override async Task<SeriesMessageResponse> DeleteSeries(SeriesId request, ServerCallContext context)
        {
            try
            {
                var response = await _series.DeleteSeries(request.Id);
                if (!response) throw new Exception("SeriesRep - PostSeries");
                return new SeriesMessageResponse() { Signal = true, Poruka = "Uspesno obrisano" };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new SeriesMessageResponse() { Signal = false, Poruka = "Greska" };
            }

        }

        public override async Task<GetRolesResponse> GerRolesSeries(SeriesId request, ServerCallContext context)
        {
            try
            {
                var roles = await _series.GetRolesSeries(request.Id);
                if (roles is null) throw new Exception("SeriesRep - GetRoles");
                List<RoleAdd> rolesAdd = new List<RoleAdd>();
                await Task.Run(() =>
                {
                    roles.ToList().ForEach((role) =>
                    {
                        var ro = _mapper.Map<Role, RoleAdd>(role);
                        rolesAdd.Add(ro);
                    });
                });
                return new GetRolesResponse() { Roles = { rolesAdd }, Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new GetRolesResponse() { Signal = false };
            }

        }

        public override async Task<SeriesMessageResponse> PostRole(RoleAdd request, ServerCallContext context)
        {
            try
            {
                var role = _mapper.Map<RoleAdd, Role>(request);
                var response = await _series.AddRole(role.Series.Id, role);
                if (!response) throw new Exception("SeriesRep - PostRole");
                return new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new SeriesMessageResponse() { Poruka = "Greska", Signal = false };
            }

        }
        public override async Task<SeriesMessageResponse> DeleteRole(SeriesRoleId request, ServerCallContext context)
        {
            try
            {
                var response = await _series.DeleteRole(request.IdSeries, request.RoleId);
                if (!response) throw new Exception("SeriesRep - DeleteRole");
                return new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new SeriesMessageResponse() { Poruka = "Greska", Signal = false };
            }

        }

        public override async Task<SeriesMessageResponse> PostActorSeries(ActorSeries request, ServerCallContext context)
        {
            try
            {
                var actorSeries = _mapper.Map<ActorSeries, Actor>(request);
                var response = await _actorRepository.AddActor(actorSeries);
                if (!response) throw new Exception("SeriesRep - PostActorSeries");
                return new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
        }
        public override async Task<SeriesMessageResponse> PutActorSeries(ActorSeriesUpdate request, ServerCallContext context)
        {
            try
            {
                var old = _mapper.Map<ActorSeries, Actor>(request.ActorOld);
                var update = _mapper.Map<ActorSeries, Actor>(request.ActorUpdate);
                var response = await _actorRepository.UpdateActor(old, update);
                if (!response) throw new Exception("SeriesRep - PutActorSeries");
                return new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
        }

        public override async Task<SeriesMessageResponse> DeleteActorSeries(ActorSeries request, ServerCallContext context)
        {
            try
            {
                var actor = _mapper.Map<ActorSeries, Actor>(request);
                var response  = await _actorRepository.DeleteActor(actor);
                if (!response) throw new Exception("SeriesRep - DeleteActorSeries");
                return new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
        }

        public override async Task<GetCountryResponse> GetAllCountry(SeriesEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _series.GetAllCountries();
                if (response is null) throw new Exception("SeriesRep - GetAllRoles");
                var full = new List<CountryFull>();
                response.ToList().ForEach((country) =>
                {
                    var coun = _mapper.Map<Country, CountryFull>(country);
                    full.Add(coun);
                });
                return new GetCountryResponse() { Countries = { full } };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                List<CountryFull> full = null;
                return new GetCountryResponse() { Countries = { full } };
            }
        }
        public override async Task<GetGenresResponse> GetAllGenre(SeriesEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _series.GetAllGenre();
                if (response is null || response.Count() == 0) throw new Exception("SeriesRep - GetAllGenre");
                var full = new List<GenreFull>();
                response.ToList().ForEach((genre) =>
                {
                    var coun = _mapper.Map<Genre, GenreFull>(genre);
                    full.Add(coun);
                });
                return new GetGenresResponse() { Genres = { full } };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new GetGenresResponse() {};
            }
        }

        public override async Task<GetRolesResponse> GetAllRoles(SeriesEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _series.GetAllRoles();
                if (response is null || response.Count() ==0) throw new Exception("SeriesRep - GetAllRoles");
                var full = new List<RoleAdd>();
                response.ToList().ForEach((role) =>
                {
                    var r = _mapper.Map<Role, RoleAdd>(role);
                    full.Add(r);
                });
                return new GetRolesResponse() { Roles = { full } };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                List<RoleAdd> full = null;
                return new GetRolesResponse() { Roles = { full } };
            }
        }






    }
}
