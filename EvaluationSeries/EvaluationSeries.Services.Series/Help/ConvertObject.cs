using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Series.Entities;
using EvaluationSeries.Services.Series.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Help
{
    public  class ConvertObject
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
        public  ActorCreate CreateActorCreate(ActorAdd actor)
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
        public  ActorAdd CreateActorAdd(ActorCreate actor)
        {
            try
            {
                return new ActorAdd()
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

        public Actor CreateActor(ActorCreate a)
        {
            try
            {
                return new Actor()
                {
                    ActorId = a.ActorId,
                    Name = a.Name,
                    Surname = a.Surname
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Series2 CreateSeries(SeriesFull ser)
        {
            try
            {
                return new Series2()
                {
                    Id = ser.Id,
                    Country = new Country() { CountryId = ser.Country.CountryId },
                    Description = ser.Description,
                    EpisodeDuration = ser.EpisodeDuration,
                    Genre = new Genre() { GenreId = ser.Genre.GenreId },
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
        public SeriesFull CreateSeriesFull(Series2 ser)
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
