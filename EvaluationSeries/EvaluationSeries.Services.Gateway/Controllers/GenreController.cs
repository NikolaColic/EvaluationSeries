using EvaluationSeries.Services.Gateway.Entities;
using EvaluationSeries.Services.Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationSeries.Services.Gateway.Controllers
{
    [Route("gateway/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private ISeriesServicesGateway _series;
        public GenreController(ISeriesServicesGateway series)
        {
            this._series = series;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            var genres = await _series.GetAllGenres();
            if (genres is null) return NotFound();
            return Ok(genres);

        }
    }
}
