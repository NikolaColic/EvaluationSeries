using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Evaluation.Entities
{
    public class EvaluationCriterion
    {
        [Key]
        public int CriteriaId { get; set; }
        public string CriteriaName { get; set; }
    }
}
