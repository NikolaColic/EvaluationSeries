using EvaluationSeries.Services.Gateway.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Services
{
    public interface IUserServicesGateway
    {
        Task<IEnumerable<User>> GetUsers();
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<User> Authenticatiion(string username, string password);
        Task<User> GetUserById(int id);






    }
}
