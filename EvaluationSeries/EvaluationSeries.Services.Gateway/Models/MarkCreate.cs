using EvaluationSeries.Services.Gateway.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Models
{
    public class MarkCreate
    {
        public int MarkId { get; set; }
        public EvaluationCriterion Criterion { get; set; }
        public int MarkValue { get; set; }
        public string MarkDescription { get; set; }
    }
}
