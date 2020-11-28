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

        public Task<bool> AddRole(Role role)
        {
            //try
            //{
            //    var response = await _series.Postro(ConvertObject.Instance.CreateSeriesFull(s));
            //    return response.Signal;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return Task.FromResult(false);
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

        public Task<bool> DeleteRole(int seriesId, int roleId)
        {
            throw new NotImplementedException();
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

        public Task<IEnumerable<Role>> GetRoles(int id)
        {
            throw new NotImplementedException();
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
