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
    [Route("gateway/countries")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ISeriesServicesGateway _series;
        public CountryController(ISeriesServicesGateway series)
        {
            this._series = series;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countries = await _series.GetAllCountries();
            if (countries is null) return NotFound();
            return Ok(countries);

        }

        
    }
}
