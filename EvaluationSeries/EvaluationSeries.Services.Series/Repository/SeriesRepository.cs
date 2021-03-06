﻿using EvaluationSeries.Services.Series.Context;
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
                if (!await RoleExist(role, 0)) return false;
                await _db.AddAsync(role);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private async Task<bool> RoleExist(Role role, int id)
        {
            var count = await _db.Role.CountAsync(r => r.Series.Id == role.Series.Id && r.Actor.ActorId == role.Actor.ActorId &&
            r.RoleId != id);
            return count >1 ? false : true;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
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
                throw;
            }

        }

        public async Task<bool> DeleteSeries(int id)
        {
            try
            {
                var series = await GetSeriesById(id);
                if (series is null) return false;

                var roles = await _db.Role.Where((el) => el.Series.Id == id).ToListAsync();
                foreach(var role in roles)
                {
                    _db.Entry(role).State = EntityState.Deleted;
                }
                _db.Entry(series).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
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
                throw;
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
                throw;
            }
        }


        public async Task<IEnumerable<Role>> GetRolesSeries(int id)
        {
            try
            {
                var roles = await _db.Role
                        .Include((r) => r.Actor)
                        .Include(r => r.Series)
                        .Include(r => r.Series.Country)
                        .Include(r => r.Series.Genre)
                        .Where((r) => r.Series.Id == id)
                        .ToListAsync();
                if (roles.Count() == 0) return null;
                return roles;
            }
            catch (Exception)
            {
                throw;
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
                _db.Entry(seriesUpdate).State = EntityState.Detached;
                _db.Update(s);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
            try
            {
                var genres = await _db.Genre.ToListAsync();
                return genres;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            try
            {
                var countries = await _db.Country.ToListAsync();
                return countries;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            try
            {
                var roles = await _db.Role
                                .Include((r) => r.Actor)
                                .Include(r => r.Series)
                                .Include(r => r.Series.Country)
                                .Include(r => r.Series.Genre)
                                .ToListAsync();
                return roles;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
