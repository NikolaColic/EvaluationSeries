using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Series.Entities;
using EvaluationSeries.Services.Series.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Help
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Series2, SeriesFull>()
                .ForMember(dest => dest.Country,
                opt => opt.MapFrom(src => new CountryFull()
                {
                    CountryId = src.Country.CountryId,
                    Name = src.Country.Name
                }))
                .ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => new GenreFull()
                {
                    GenreId = src.Genre.GenreId,
                    GenreName = src.Genre.GenreName
                }));

            CreateMap<SeriesFull, Series2>()
               .ForMember(dest => dest.Country,
               opt => opt.MapFrom(src => new Country()
               {
                   CountryId = src.Country.CountryId,
                   Name = src.Country.Name
               }))
               .ForMember(dest => dest.Genre,
               opt => opt.MapFrom(src => new Genre()
               {
                   GenreId = src.Genre.GenreId,
                   GenreName = src.Genre.GenreName
               }));

            CreateMap<ActorSeries, Actor>();
            CreateMap<Actor, ActorSeries>();
            CreateMap<ActorCreate, ActorAddSeries>();
            CreateMap<ActorAddSeries, ActorCreate>();
            CreateMap<ActorCreate, Actor>();
            CreateMap<ActorAdd, ActorCreate>();
            CreateMap<ActorCreate, ActorAdd>();
            CreateMap<Country, CountryFull>();
            CreateMap<Genre, GenreFull>(); 


            CreateMap<Role, RoleAdd>()
                .ForMember((dest) => dest.Actor,
                opt => opt.MapFrom(s => s.Actor))
                .ForMember((dest) => dest.Series,
                opt => opt.MapFrom(s => s.Series));
            CreateMap<RoleAdd, Role>()
                .ForMember((dest) => dest.Actor,
                opt => opt.MapFrom(s => s.Actor));

        }

    }
}
