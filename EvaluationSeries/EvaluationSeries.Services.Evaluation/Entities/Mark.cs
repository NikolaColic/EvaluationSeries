using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Evaluation.Entities
{
    public class Mark
    {
        [Key]
        public int MarkId { get; set; }
        [Key, ForeignKey("CriteriaId")]
        public EvaluationCriterion Criterion { get; set; }
        [Key, ForeignKey("EvaluationId")]
        public Evaluation2 Evaluation { get; set; }
        public int MarkValue { get; set; }
        public string MarkDescription { get; set; }
    }
}
