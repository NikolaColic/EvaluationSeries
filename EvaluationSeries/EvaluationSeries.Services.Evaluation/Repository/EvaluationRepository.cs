using EvaluationSeries.Services.Evaluation.Context;
using EvaluationSeries.Services.Evaluation.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Evaluation.Repository
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly EvaluationDbContext _db;
        public EvaluationRepository(EvaluationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> DeleteEvaluation(int id)
        {
            try
            {
                var evaluation = await GetEvaluationById(id);
                if (evaluation is null) return false;
                var response = await DeleteMarks(id);
                if (!response) return false;
                _db.Entry(evaluation).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> DeleteMarks(int evaluationId)
        {
            try
            {
                var marks = await _db.Mark
                    .Where((mar) => mar.Evaluation.EvaluationId == evaluationId)
                    .ToListAsync();
                foreach(var mark in marks)
                {
                    if (mark is null) return false;
                    _db.Entry(mark).State = EntityState.Deleted;
                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSeries(Series s)
        {
            try
            {
                //eventualno da obrisem sve evaluacije za ovu seriju
                var series = await _db.Series.SingleOrDefaultAsync((ser) => ser.Name == s.Name);
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

        public async Task<bool> DeleteUser(User u)
        {
            try
            {
                //eventualno obrisati sve evaluacije za ovog korisnika
                var user = await _db.User.SingleOrDefaultAsync((ser) => ser.Username == u.Username);
                if (user is null) return false;
                _db.Entry(user).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async  Task<IEnumerable<EvaluationCriterion>> GetAllCriterions()
        {
            try
            {
                var criterions = await _db.Criteria.ToListAsync();
                return criterions;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Evaluation2>> GetAllEvalutions()
        {
            try
            {
                var evaluations = await _db.Evaluation
                        .Include((e) => e.Series)
                        .Include((e) => e.User)
                        .ToListAsync();
                return evaluations;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Mark>> GetAllMarks()
        {
            try
            {
                var marks = await _db.Mark
                        .Include((e) => e.Evaluation)
                        .Include((e) => e.Evaluation.Series)
                        .Include((e) => e.Evaluation.User)
                        .Include((e) => e.Criterion)
                        .ToListAsync();
                return marks;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Evaluation2> GetEvaluationById(int id)
        {
            try
            {
                var evaluation = await _db.Evaluation
                                .Include((e) => e.Series)
                                .Include((e) => e.User)
                                .SingleOrDefaultAsync((e) => e.EvaluationId == id);
                return evaluation;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> PostEvaluation(Evaluation2 evaluation)
        {
            try
            {
                var series = await _db.Series.SingleOrDefaultAsync((s) => s.Id == evaluation.Series.Id);
                var user = await _db.User.SingleOrDefaultAsync((s) => s.Id == evaluation.User.Id);
                if (series is null || user is null) return false;

                evaluation.Series = series;
                evaluation.User = user;

                await _db.AddAsync(evaluation);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> PostMarks(List<Mark> marks)
        {
            try
            {
                foreach(var mark in marks)
                {
                    var evaluation = await _db.Evaluation.SingleOrDefaultAsync((s) => s.EvaluationId == mark.Evaluation.EvaluationId);
                    var criterion = await _db.Criteria.SingleOrDefaultAsync((s) => s.CriteriaId == mark.Criterion.CriteriaId);
                    if (evaluation is null || criterion is null) return false;
                    
                    mark.Evaluation = evaluation;
                    mark.Criterion = criterion;
                    await _db.AddAsync(mark);
                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PostSeries(Series s)
        {
            try
            {
                await _db.AddAsync(s);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PostUser(User u)
        {
            try
            {
                await _db.AddAsync(u);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PutMarks(List<Mark> marks)
        {
            try
            {
                foreach (var mark in marks)
                {
                    var markUpdate = await _db.Mark.SingleOrDefaultAsync((m) => m.MarkId == mark.MarkId);
                    var evaluation = await _db.Evaluation.SingleOrDefaultAsync((s) => s.EvaluationId == mark.Evaluation.EvaluationId);
                    var criterion = await _db.Criteria.SingleOrDefaultAsync((s) => s.CriteriaId == mark.Criterion.CriteriaId);
                    if (evaluation is null || criterion is null) return false;
                    mark.Evaluation = evaluation;
                    mark.Criterion = criterion;

                    if (markUpdate is null) await _db.AddAsync(mark);
                    else _db.Entry(markUpdate).CurrentValues.SetValues(mark);
                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PutSeries(Series s, Series update)
        {
            try
            {
                var seriesUpdate = await _db.Series.SingleOrDefaultAsync((ser) => ser.Name == update.Name);
                if (seriesUpdate is null) return false;
                s.Id = seriesUpdate.Id;
                _db.Entry(seriesUpdate).CurrentValues.SetValues(s);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PutUser(User u, User update)
        {
            try
            {
                var userUpdate = await _db.User.SingleOrDefaultAsync((ser) => ser.Username == update.Username);
                if (userUpdate is null) return false;
                u.Id = userUpdate.Id;
                _db.Entry(userUpdate).CurrentValues.SetValues(u);
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
