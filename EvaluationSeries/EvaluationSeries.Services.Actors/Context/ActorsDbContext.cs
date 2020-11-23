using EvaluationSeries.Services.Actors.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Actors.Context
{
    public class ActorsDbContext : DbContext
    {
        public ActorsDbContext(DbContextOptions<ActorsDbContext> options) : base(options)
        {

        }
        public DbSet<Actor> Actor { get; set; }
    }
}
