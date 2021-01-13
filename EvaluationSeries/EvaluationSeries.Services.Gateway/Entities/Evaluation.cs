using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Entities
{
    public class Evaluation
    {
        public int EvaluationId { get; set; }
        public User User { get; set; }
        public Series Series { get; set; }
        public string Advantage { get; set; }
        public string Flaw { get; set; }
        public bool Favourite { get; set; }

    }
}
