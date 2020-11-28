using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Entities
{
    public class Mark
    {
        public int MarkId { get; set; }
        public EvaluationCriterion Criterion { get; set; }
        public Evaluation Evaluation { get; set; }
        public int MarkValue { get; set; }
        public string MarkDescription { get; set; }
    }
}
