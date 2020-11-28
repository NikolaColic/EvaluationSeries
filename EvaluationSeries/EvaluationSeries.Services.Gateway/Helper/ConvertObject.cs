using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Helper
{
    public class ConvertObject
    {
        private static ConvertObject instance;
        private ConvertObject()
        {

        }
        public static ConvertObject Instance
        {
            get
            {
                if (instance == null) instance = new ConvertObject();
                return instance;
            }
        }
        public Series CreateSeries(SeriesFull ser)
        {
            try
            {
                return new Series()
                {
                    Id = ser.Id,
                    Country = new Country() { CountryId = ser.Country.CountryId, Name = ser.Country.Name },
                    Description = ser.Description,
                    EpisodeDuration = ser.EpisodeDuration,
                    Genre = new Genre() { GenreId = ser.Genre.GenreId, GenreName = ser.Genre.GenreName },
                    LogoUrl = ser.LogoUrl,
                    Name = ser.Name,
                    NumberSeason = ser.NumberSeason,
                    WebSiteUrl = ser.WebSiteUrl,
                    Year = ser.Year
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
        public SeriesFull CreateSeriesFull(Series ser)
        {
            try
            {
                return new SeriesFull()
                {
                    Id = ser.Id,
                    Description = ser.Description,
                    EpisodeDuration = ser.EpisodeDuration,
                    LogoUrl = ser.LogoUrl,
                    Name = ser.Name,
                    NumberSeason = ser.NumberSeason,
                    WebSiteUrl = ser.WebSiteUrl,
                    Year = ser.Year,
                    Country = new CountryFull()
                    {
                        CountryId = ser.Country.CountryId,
                        Name = ser.Country.Name
                    },
                    Genre = new GenreFull()
                    {
                        GenreId = ser.Genre.GenreId,
                        GenreName = ser.Genre.GenreName
                    }
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
