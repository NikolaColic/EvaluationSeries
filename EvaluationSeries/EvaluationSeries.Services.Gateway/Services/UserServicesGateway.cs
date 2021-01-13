using AutoMapper;
using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Gateway.Entities;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Services
{
    public class UserServicesGateway : IUserServicesGateway
    {
        private UserGrpc.UserGrpcClient _user;

        private IMapper _mapper;
        private readonly ILogger<UserServicesGateway> _logger;

        public UserServicesGateway(IMapper mapper,ILogger<UserServicesGateway> logger)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:5002");
            this._user = new UserGrpc.UserGrpcClient(channel);
            _mapper = mapper;
            this._logger = logger;
        }
        public async Task<bool> AddUser(User user)
        {
            try
            {
                var userAdd = _mapper.Map<User, UserAdd>(user);
                var response = await _user.PostUserAsync(userAdd);
                return response.Signal;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return false;
            }

        }

        public async Task<User> Authenticatiion(string username, string password)
        {
            try
            {
                var response = await _user.AuthenticationAsync(new UserAuthentication() { Password = password, Username = username });
                if (!response.Signal) return null;
                var user = _mapper.Map<UserAdd, User>(response.User);
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }

        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var response = await _user.DeleteUserAsync(new UserId() { Id = id});
                return response.Signal;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return false;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                var users = await GetUsers();
                var user =  users.SingleOrDefault((s) => s.Id == id);
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                var response = await _user.GetUsersAsync(new UserEmpty());
                if (response.Users is null || response.Users.Count == 0) return null;
                var users = new List<User>();
                response.Users.ToList().ForEach((user) =>
                {
                    var u = _mapper.Map<UserAdd, User>(user);
                    users.Add(u);
                });
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return null;
            }
        }
        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                var userAdd = _mapper.Map<User, UserAdd>(user);
                var response = await _user.PutUserAsync(userAdd);
                return response.Signal;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR");
                return false;
            }
        }
    }
}
