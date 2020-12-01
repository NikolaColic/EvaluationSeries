using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Help
{
    public class GatewayProfile : Profile
    {
        public GatewayProfile()
        {
            CreateMap<Series, SeriesFull>()
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

            CreateMap<SeriesFull, Series>()
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
            CreateMap<Actor, ActorSeries>();
            CreateMap<Actor, ActorAddSeries>();
            CreateMap<ActorAddSeries, Actor>();

            CreateMap<Role, RoleAdd>()
                .ForMember((dest) => dest.Actor,
                opt => opt.MapFrom(s => s.Actor))
                .ForMember((dest) => dest.Series,
                opt => opt.MapFrom(s => s.Series));
            CreateMap<RoleAdd, Role>()
                .ForMember((dest) => dest.Actor,
                opt => opt.MapFrom(s => s.Actor));
            CreateMap<UserAdd, User>()
              .ForMember((dest) => dest.Country,
              opt => opt.MapFrom((s) => new Country() { CountryId = s.Country.CountryId, Name = s.Country.Name }));
            CreateMap<User, UserAdd>()
                .ForMember((dest) => dest.Country,
                opt => opt.MapFrom((s) => new CountryAdd() { CountryId = s.Country.CountryId, Name = s.Country.Name }));
            CreateMap<CountryFull, Country>();
            CreateMap<GenreFull, Genre>();
        }
    }
}
