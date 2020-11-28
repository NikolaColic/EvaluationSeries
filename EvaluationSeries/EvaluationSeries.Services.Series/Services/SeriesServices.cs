using EvaluationSeries.Grpc;
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
        private ActorServices _actor;
        private IActorRepository _actorRepository;

        public SeriesServices(ISeriesRepository series, IActorRepository actorRepository )
        {
            this._actorRepository = actorRepository;
            this._series = series;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            this._actor = new ActorServices(new ActorsGrpc.ActorsGrpcClient(channel));
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
                    var sFull = ConvertObject.Instance.CreateSeriesFull(ser);
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
                SeriesFull full = ConvertObject.Instance.CreateSeriesFull(series);
                return new GetSeriesByIdResponse() { Series = full, Signal = true };
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
                var series = ConvertObject.Instance.CreateSeries(request);
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
                var series = ConvertObject.Instance.CreateSeries(request);
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
                        RoleAdd ro = ConvertObject.Instance.CreateRoleAdd(role);
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
                var role = ConvertObject.Instance.CreateRole(request);
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
                var response = await _series.DeleteRole(request.SeriesId, request.RoleId);
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
                if (actors is null) return new GetActorsSeriesResponse() { Actors = { actors } };
                actors = new List<ActorAddSeries>();
                response.ToList().ForEach((act) =>
                {
                    var nov = ConvertObject.Instance.CreateActorAddSeries(act);
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
                var actor = Create(request);
                var response = await _actor.PostActor(actor);
                if (!response) return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };
                if (await _actorRepository.AddActor(ConvertObject.Instance.CreateActor(actor))) return new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true };
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

                var actor = Create(request);
                var response = await _actor.PutActor(actor);
                if (!response) return new SeriesMessageResponse() { Poruka = "Neuspesno", Signal = false };

                if (await _actorRepository.UpdateActor(actorUpdate, ConvertObject.Instance.CreateActor(actor))) return new SeriesMessageResponse() { Poruka = "Uspesno", Signal = true };
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

        private ActorCreate Create(ActorAddSeries actor)
        {
            try
            {
                return new ActorCreate()
                {
                    ActorId = actor.ActorId,
                    Biography = actor.Biography,
                    ImageUrl = actor.ImageUrl,
                    Name = actor.Name,
                    Surname = actor.Surname,
                    WikiUrl = actor.WikiUrl

                };
            }
            catch (Exception)
            {
                return null;
            }

        }
            



    }
}
