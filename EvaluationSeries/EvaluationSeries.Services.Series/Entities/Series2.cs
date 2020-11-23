using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Entities
{
    public class Series2
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberSeason { get; set; }
        public int EpisodeDuration { get; set; }
        public string WebSiteUrl { get; set; }
        public string LogoUrl { get; set; }
        public int Year { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

    }
}
