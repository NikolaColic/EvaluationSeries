﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Actors.Entities
{
    public class Actor 
    {
        [Key]
        public int ActorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Biography { get; set; }
        public string WikiUrl { get; set; }
        public string ImageUrl { get; set; }


    }
}
