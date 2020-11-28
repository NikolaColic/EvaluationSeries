using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Series.Help;
using EvaluationSeries.Services.Series.Repository;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Services
{
    public class SeriesServices : SeriesGrpc.SeriesGrpcBase
    {
        private ISeriesRepository _series;
        public SeriesServices(ISeriesRepository series)
        {
            this._series = series;
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

    }
}
