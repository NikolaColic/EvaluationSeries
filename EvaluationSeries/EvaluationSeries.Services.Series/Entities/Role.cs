using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Key, ForeignKey("ActorId")]
        public Actor Actor { get; set; }
        [Key,ForeignKey("Id")]
        public Series2 Series { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }


    }
}
