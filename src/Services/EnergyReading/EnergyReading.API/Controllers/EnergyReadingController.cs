using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EnergyReading.API.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace EnergyReading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyReadingController : ControllerBase
    {
        readonly IEnergyReadingQueries _queries;
        private readonly ILogger _logger;

        public EnergyReadingController(IEnergyReadingQueries queries, ILogger<EnergyReadingController> logger)
        {
            this._logger = logger;
            this._queries = queries;

            _logger.LogDebug("Request has started");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EnergyReadingGroup>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get([FromQuery, Required, Range(1,100)] byte percvalue)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid request: {ModelState}");
                return BadRequest(ModelState);
            }

            _logger.LogDebug("Successful request");

            return Ok( _queries.FindDivergingReadings(percvalue));
        }
    }
}
