using EvaluationSeries.Services.Evaluation.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Evaluation.Context
{
    public class EvaluationDbContext : DbContext
    {
        public EvaluationDbContext(DbContextOptions<EvaluationDbContext> options) : base(options)
        {

        }
        public DbSet<Series> Series { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Evaluation2> Evaluation { get; set; }
        public DbSet<EvaluationCriterion> Criteria { get; set; }
        public DbSet<Mark> Mark { get; set; }




    }
}
