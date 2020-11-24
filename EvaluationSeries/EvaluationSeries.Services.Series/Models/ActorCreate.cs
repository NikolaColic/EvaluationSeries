using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Models
{
    public class ActorCreate
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Biography { get; set; }
        public string WikiUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
