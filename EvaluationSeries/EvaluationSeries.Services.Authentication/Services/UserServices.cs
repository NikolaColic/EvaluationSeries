using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Authentication.Entities;
using EvaluationSeries.Services.Authentication.Repository;
using Grpc.Core;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserServices> _logger;

        public UserServices(IUserRepository user, IMapper mapper, ILogger<UserServices> logger)
        {
            _user = user;
            _mapper = mapper;
            this._logger = logger;
        }
        public override async Task<UsersResponse> GetUsers(UserEmpty request, ServerCallContext context)
        {
            try
            {
                var response = await _user.GetAllUsers();
                if (response is null || response.Count() == 0) throw new Exception("Prazna lista");
                var usersAdd = new List<UserAdd>();
                response.ToList().ForEach((user) =>
                {
                    var userAdd = _mapper.Map<User, UserAdd>(user);
                    usersAdd.Add(userAdd);
                });
                return new UsersResponse() { Users = { usersAdd } };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new UsersResponse() { };
            }
        }
        public override async Task<UserAuthenticationResponse> Authentication(UserAuthentication request, ServerCallContext context)
        {
            try
            {
                var response = await _user.Authentication(request.Username, request.Password);
                if (response is null) throw new Exception("Nije pronasao korisnika!");
                var user = _mapper.Map<User, UserAdd>(response);
                return new UserAuthenticationResponse() { Signal = true, User = user };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new UserAuthenticationResponse() { Signal = false, User = null };
            }
        }
        public override async Task<UserMessageResponse> PostUser(UserAdd request, ServerCallContext context)
        {
            try
            {
                var user = _mapper.Map<UserAdd, User>(request);
                var response = await _user.AddUser(user);
                if (!response) throw new Exception("Nije uspesno dodao korisnika");
                return new UserMessageResponse() { Poruka = "Uspesno", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new UserMessageResponse() { Poruka = "Greska", Signal = false };
            } 
        }
        public override async  Task<UserMessageResponse> PutUser(UserAdd request, ServerCallContext context)
        {
            try
            {
                var user = _mapper.Map<UserAdd, User>(request);
                var response = await _user.UpdateUser(user);
                if (response) throw new Exception("Neuspesna izmena");
                return new UserMessageResponse() { Poruka = "Uspesno", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new UserMessageResponse() { Poruka = "Greska", Signal = false };
            }
        }

        public override async Task<UserMessageResponse> DeleteUser(UserId request, ServerCallContext context)
        {
            try
            {
                var response = await _user.DeleteUser(request.Id);
                if (!response) throw new Exception("Nije uspeo da obrise korisnika!");
                return new UserMessageResponse() { Poruka = "Uspesno", Signal = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return new UserMessageResponse() { Poruka = "Greska", Signal = false };
            }
        }
    }
}
