using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Gateway.Entities
{
    public class Role
    {
        
        public int RoleId { get; set; }
        public Actor Actor { get; set; }
        public Series Series { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }


    }
}
