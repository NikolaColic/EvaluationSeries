using EvaluationSeries.Services.Authentication.Context;
using EvaluationSeries.Services.Authentication.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Authentication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _db;
        public UserRepository(UserDbContext user)
        {
            _db = user;
        }
        public async Task<bool> AddUser(User user)
        {
            try
            {
                var country = await _db.Country.SingleOrDefaultAsync((c) => c.CountryId == user.Country.CountryId);
                if (country is null) return false;
                user.Country = country;
                await _db.AddAsync(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<User> Authentication(string username, string password)
        {
            try
            {
                var user = await _db.User
                                        .Include((u) => u.Country)
                                        .SingleOrDefaultAsync((us) => us.Username == username && us.Password == password);
                if (user is null) return null;
                return user;
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var userDelete = await GetUserById(id);
                if (userDelete is null) return false;

                _db.Entry(userDelete).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return null;
                var users = await _db.User
                        .Include((u) => u.Country)
                        .ToListAsync();
                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<User> GetUserById(int id)
        {
            try
            {
                var user = await _db.User
                                .Include((u) => u.Country)
                                .SingleOrDefaultAsync((us) => us.Id == id);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                var userUpdate = await GetUserById(user.Id);
                if (userUpdate is null) return false;
                var country = await _db.Country.SingleOrDefaultAsync((c) => c.CountryId == user.Country.CountryId);
                if (country is null) return false;
                user.Country = country;

                _db.Entry(userUpdate).State = EntityState.Detached;
                _db.Update(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
