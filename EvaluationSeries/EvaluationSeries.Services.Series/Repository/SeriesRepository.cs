using EvaluationSeries.Services.Series.Context;
using EvaluationSeries.Services.Series.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Repository
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly SeriesDbContext _db;
        public SeriesRepository(SeriesDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddRole(int id, Role role)
        {
            try
            {
                role = await SetObjectRole(id, role);
                if (role is null) return false;
                await _db.AddAsync(role);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private async Task<Role> SetObjectRole(int id, Role role)
        {
            try
            {
                var series = await GetSeriesById(id);
                var actor = await _db.Actor.SingleOrDefaultAsync((a) => a.ActorId == role.Actor.ActorId);
                if (series is null || actor is null) return null;
                role.Series = series;
                role.Actor = actor;
                return role;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<bool> AddSeries(Series2 s)
        {
            try
            {
                s = await SetObjects(s);
                if (s is null) return false; 

                await _db.AddAsync(s);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private async Task<Series2> SetObjects(Series2 s)
        {
            try
            {
                var genre = await GetGenreById(s.Genre.GenreId);
                var country = await GetCountryById(s.Country.CountryId);
                if (genre is null || country is null) return null;
                s.Country = country;
                s.Genre = genre;
                return s;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<Genre> GetGenreById(int id)
        {
            try
            {
                var genre = await _db.Genre.SingleOrDefaultAsync((g) => g.GenreId == id);
                return genre;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<Country> GetCountryById(int id)
        {
            try
            {
                var country = await _db.Country.SingleOrDefaultAsync((g) => g.CountryId == id);
                return country;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteRole(int seriesId, int roleId)
        {
            try
            {
                var series = await GetSeriesById(seriesId);
                if (series is null) return false;

                var role = await _db.Role.SingleOrDefaultAsync((r) => r.RoleId == roleId);
                _db.Entry(role).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> DeleteSeries(int id)
        {
            try
            {
                var series = await GetSeriesById(id);
                if (series is null) return false;
                _db.Entry(series).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false; 
            }

        }

        public async Task<Series2> GetSeriesById(int id)
        {
            try
            {
                var series = await _db.Series
                    .Include(s=> s.Genre)
                    .Include(s => s.Country)
                    .SingleOrDefaultAsync((s) => s.Id == id);
                return series;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Series2>> GetAllSeries()
        {
            try
            {
                var series = await _db.Series
                    .Include(s => s.Genre)
                    .Include(s => s.Country)
                    .ToListAsync();
                return series;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<IEnumerable<Role>> GetRolesSeries(int id)
        {
            try
            {
                var roles = await _db.Role
                        .Include((r) => r.Actor)
                        .Include(r => r.Series)
                        .Where((r) => r.Series.Id == id)
                        .ToListAsync();
                if (roles.Count() == 0) return null;
                return roles;
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        public async Task<bool> UpdateSeries(Series2 s)
        {
            try
            {
                var seriesUpdate = await GetSeriesById(s.Id);
                if (seriesUpdate is null) return false;
                s = await SetObjects(s);
                if (s is null) return false;
                _db.Entry(seriesUpdate).CurrentValues.SetValues(s);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
