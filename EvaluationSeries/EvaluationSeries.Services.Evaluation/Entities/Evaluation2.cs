using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Evaluation.Entities
{
    public class Evaluation2
    {
        [Key]
        public int EvaluationId { get; set; }
        [Key, ForeignKey("Id")]
        public User User { get; set; }
        [Key, ForeignKey("SeriesId")]
        public Series Series { get; set; }
        public string Advantage { get; set; }
        public string Flaw { get; set; }
        public bool Favourite { get; set; }
    }
}
