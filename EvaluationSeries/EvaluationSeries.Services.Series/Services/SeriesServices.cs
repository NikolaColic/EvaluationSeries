using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Series.Entities;
using EvaluationSeries.Services.Series.Help;
using EvaluationSeries.Services.Series.Models;
using EvaluationSeries.Services.Series.Repository;
using Grpc.Core;
using Grpc.Net.Client;
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
        private IActorServices _actor;
        private IActorRepository _actorRepository;
        private IMapper _mapper;

        public SeriesServices(ISeriesRepository series, IActorRepository actorRepository,
            IMapper mapper, IActorServices actor)
        {
            this._actorRepository = actorRepository;
            this._series = series;
            this._mapper = mapper;
            this._actor = actor;

        }
        public override async Task<GetSeriesResponse> GetAllSeries(SeriesEmpty request, ServerCallContext context)
        {
            try
            {
                var series = await _series.GetAllSeries();
                if (series is null) return new GetSeriesResponse() { Signal = false };
                List<SeriesFull> seriesFull = new List<SeriesFull>();
                series.ToList().ForEach((ser) =>
                {
                    var sFull = _mapper.Map<Series2, SeriesFull>(ser);
                    seriesFull.Add(sFull);
                });
                return new GetSeriesResponse() { Series = { seriesFull }, Signal = true };
            }
            catch (Exception)
            {
                return new GetSeriesResponse() { Signal = false };
            }

        }
        public override async Task<GetSeriesByIdResponse> GetSeriesById(SeriesId request, ServerCallContext context)
        {
            try
            {
                var series = await _series.GetSeriesById(request.Id);
                if (series is null) return  new GetSeriesByIdResponse() { Series = null, Signal = false };
                var seriesFull = _mapper.Map<Series2, SeriesFull>(series);
                return new GetSeriesByIdResponse() { Series = seriesFull, Signal = true };
            }
            catch (Exception)
            {
                return new GetSeriesByIdResponse() { Series = null, Signal = false };
            }

        }

        public override async Task<SeriesMessageResponse> PostSeries(SeriesFull request, ServerCallContext context)
        {
            try
            {
                var series = _mapper.Map<SeriesFull, Series2>(request);
                var response = await _series.AddSeries(series);
                return response ? new SeriesMessageResponse() { Signal = true, Poruka = "Uspesno sacuvano" } :
                    new SeriesMessageResponse() { Signal = false, Poruka = "Neuspesno sacuvnao" };
            }
            catch (Exception)
            {
                return new SeriesMessageResponse() { Signal = false, Poruka = "Greska" };
            }
        }
        public override async Task<SeriesMessageResponse> PutSeries(SeriesFull request, ServerCallContext context)
        {
            try
            {
                var series = _mapper.Map<SeriesFull, Series2>(request);
                var response = await _series.UpdateSeries(series);
                return response ? new SeriesMessageResponse() { Signal = true, Poruka = "Uspesno izmenjeno" } :
                    new SeriesMessageResponse() { Signal = false, Poruka = "Neuspesno azurirano" };
            }
            catch (Exception)
            {
               return new SeriesMessageResponse() { Signal = false, Poruka = "Greska" };
            }
        }
        public override async Task<SeriesMessageResponse> DeleteSeries(SeriesId request, ServerCallContext context)
        {
            try
            {
                var response = await _series.DeleteSeries(request.Id);
                return response ? new SeriesMessageResponse() { Signal = true, Poruka = "Uspesno obrisano" } :
                        new SeriesMessageResponse() { Signal = false, Poruka = "Neuspesno obrisano" };
            }
            catch (Exception)
            {
                return new SeriesMessageResponse() { Signal = false, Poruka = "Greska" };
            }

        }

        public override async Task<GetRolesResponse> GerRolesSeries(SeriesId request, ServerCallContext context)
        {
            try
            {
                var roles = await _series.GetRolesSeries(request.Id);
                if (roles is null) return new GetRolesResponse() { Signal = false };
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
            catch (Exception)
            {
                return new GetRolesResponse() { Signal = false };
            }

        }

        public override async Task<SeriesMessageResponse> PostRole(RoleAdd request, ServerCallContext context)
        {
            try
            {
                var role = _mapper.Map<RoleAdd, Role>(request);
                var response = await _series.AddRole(role.Series.Id, role);
                return response ? new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true }
                : new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
            catch (Exception)
            {
                return new SeriesMessageResponse() { Poruka = "Greska", Signal = false };
            }

        }
        public override async Task<SeriesMessageResponse> DeleteRole(SeriesRoleId request, ServerCallContext context)
        {
            try
            {
                var response = await _series.DeleteRole(request.IdSeries, request.RoleId);
                return response ? new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true }
                    : new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
            catch (Exception)
            {
                return new SeriesMessageResponse() { Poruka = "Greska", Signal = false };
            }

        }

        public override async Task<GetActorsSeriesResponse> GetAllActors(SeriesEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _actor.GetAll();
                List<ActorAddSeries> actors = null;
                if (response is null) return new GetActorsSeriesResponse() { Actors = { actors } };
                actors = new List<ActorAddSeries>();
                response.ToList().ForEach((act) =>
                {
                    var nov = _mapper.Map<ActorCreate, ActorAddSeries>(act);
                    actors.Add(nov);
                });
                return new GetActorsSeriesResponse() { Actors = { actors } };
            }
            catch (Exception)
            {
                List<ActorAddSeries> actorError = null;
                return new GetActorsSeriesResponse() { Actors = { actorError } };
            }
        }

        public override async Task<SeriesMessageResponse> PostActorSeries(ActorAddSeries request, ServerCallContext context)
        {
            try
            {
                var actor = _mapper.Map<ActorAddSeries, ActorCreate>(request);
                var response = await _actor.PostActor(actor);
                if (!response) return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };

                var actorSeries = _mapper.Map<ActorCreate, Actor>(actor);
                if (await _actorRepository.AddActor(actorSeries)) return new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true };
                return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };

            }
            catch (Exception)
            {
                return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
        }
        public override async Task<SeriesMessageResponse> PutActorSeries(ActorAddSeries request, ServerCallContext context)
        {
            try
            {
                var actorUpdate = await _actor.GetActorById(request.ActorId);
                if (actorUpdate is null) return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };

                var actor = _mapper.Map<ActorAddSeries, ActorCreate>(request);

                var response = await _actor.PutActor(actor);
                if (!response) return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };

                var actorSeries = _mapper.Map<ActorCreate, Actor>(actor);
                if (await _actorRepository.UpdateActor(actorUpdate, actorSeries)) return new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true };
                return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };

            }
            catch (Exception)
            {
                return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
        }

        public override async Task<SeriesMessageResponse> DeleteActorSeries(SeriesId request, ServerCallContext context)
        {
            try
            {
                var actorDelete = await _actor.GetActorById(request.Id);
                if (actorDelete is null) return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };

                var response = await _actor.DeleteActor(request.Id);
                if (!response) return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };

                if (await _actorRepository.DeleteActor(actorDelete)) return new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true };
                return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
            catch (Exception)
            {
                return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
        }

        public override async Task<GetCountryResponse> GetAllCountry(SeriesEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _series.GetAllCountries();
                List<CountryFull> full = null;
                if (response is null) return new GetCountryResponse() { Countries = { full } };
                full = new List<CountryFull>();
                response.ToList().ForEach((country) =>
                {
                    var coun = _mapper.Map<Country, CountryFull>(country);
                    full.Add(coun);
                });
                return new GetCountryResponse() { Countries = { full } };
            }
            catch (Exception)
            {
                List<CountryFull> full = null;
                return new GetCountryResponse() { Countries = { full } };
            }
        }
        public override async Task<GetGenresResponse> GetAllGenre(SeriesEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _series.GetAllGenre();
                List<GenreFull> full = null;
                if (response is null) return new GetGenresResponse() { Genres = { full } };
                full = new List<GenreFull>();
                response.ToList().ForEach((genre) =>
                {
                    var coun = _mapper.Map<Genre, GenreFull>(genre);
                    full.Add(coun);
                });
                return new GetGenresResponse() { Genres = { full } };
            }
            catch (Exception)
            {
                List<GenreFull> full = null;
                return new GetGenresResponse() { Genres = { full } };
            }
        }

        public override async Task<GetRolesResponse> GetAllRoles(SeriesEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _series.GetAllRoles();
                List<RoleAdd> full = null;
                if (response is null) return new GetRolesResponse() { Roles = { full } };
                full = new List<RoleAdd>();
                response.ToList().ForEach((role) =>
                {
                    var r = _mapper.Map<Role, RoleAdd>(role);
                    full.Add(r);
                });
                return new GetRolesResponse() { Roles = { full } };
            }
            catch (Exception)
            {
                List<RoleAdd> full = null;
                return new GetRolesResponse() { Roles = { full } };
            }
        }






    }
}
