using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Authentication.Entities;
using EvaluationSeries.Services.Authentication.Repository;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace EvaluationSeries.Services.Authentication.Services
{
    public class UserServices : UserGrpc.UserGrpcBase
    {
        private IUserRepository _user;
        private IMapper _mapper;
        public UserServices(IUserRepository user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }
        public override async Task<UsersResponse> GetUsers(UserEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _user.GetAllUsers();
                if (response is null || response.Count() == 0) return new UsersResponse() {};
                var usersAdd = new List<UserAdd>();
                response.ToList().ForEach((user) =>
                {
                    var userAdd = _mapper.Map<User, UserAdd>(user);
                    usersAdd.Add(userAdd);
                });
                return new UsersResponse() { Users = { usersAdd } };
            }
            catch (Exception)
            {
                return new UsersResponse() {};
            }
        }
        public override async Task<UserAuthenticationResponse> Authentication(UserAuthentication request, ServerCallContext context)
        {
            try
            {
                var response = await _user.Authentication(request.Username, request.Password);
                if (response is null) return new UserAuthenticationResponse() { Signal = false, User = null };
                var user = _mapper.Map<User, UserAdd>(response);
                return new UserAuthenticationResponse() { Signal = true, User = user };
            }
            catch (Exception)
            {
                return new UserAuthenticationResponse() { Signal = false, User = null };
            }
        }
        public override async Task<UserMessageResponse> PostUser(UserAdd request, ServerCallContext context)
        {
            try
            {
                var user = _mapper.Map<UserAdd, User>(request);
                var response = await _user.AddUser(user);
                if (response) return new UserMessageResponse() { Poruka = "Uspesno", Signal = true };
                return new UserMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
            catch (Exception)
            {
                return new UserMessageResponse() { Poruka = "Greska", Signal = false };
            } 
        }
        public override async  Task<UserMessageResponse> PutUser(UserAdd request, ServerCallContext context)
        {
            try
            {
                var user = _mapper.Map<UserAdd, User>(request);
                var response = await _user.UpdateUser(user);
                if (response) return new UserMessageResponse() { Poruka = "Uspesno", Signal = true };
                return new UserMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
            catch (Exception)
            {
                return new UserMessageResponse() { Poruka = "Greska", Signal = false };
            }
        }

        public override async Task<UserMessageResponse> DeleteUser(UserId request, ServerCallContext context)
        {
            try
            {
                var response = await _user.DeleteUser(request.Id);
                if (response) return new UserMessageResponse() { Poruka = "Uspesno", Signal = true };
                return new UserMessageResponse() { Poruka = "Neuspesno", Signal = false };
            }
            catch (Exception)
            {
                return new UserMessageResponse() { Poruka = "Greska", Signal = false };
            }
        }
    }
}
