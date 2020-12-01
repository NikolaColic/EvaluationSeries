using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Authentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Authentication.Help
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAdd, User>()
                .ForMember((dest) => dest.Country,
                opt => opt.MapFrom((s) => new Country() {CountryId = s.Country.CountryId,Name = s.Country.Name }));
            CreateMap<User, UserAdd>()
                .ForMember((dest) => dest.Country,
                opt => opt.MapFrom((s) => new CountryAdd() { CountryId = s.Country.CountryId, Name = s.Country.Name }));


        }
    }
}
