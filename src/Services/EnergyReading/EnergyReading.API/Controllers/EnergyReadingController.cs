using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EnergyReading.API.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EnergyReading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyReadingController : ControllerBase
    {
        readonly IEnergyReadingQueries _queries;

        public EnergyReadingController(IEnergyReadingQueries queries)
        {
            //this.Logger = logger;
            this._queries = queries;

            //this.BucketName = configuration[Startup.AppS3BucketKey];
            //if(string.IsNullOrEmpty(this.BucketName))
            //{
            //    logger.LogCritical("Missing configuration for S3 bucket. The AppS3Bucket configuration must be set to a S3 bucket.");
            //    throw new Exception("Missing configuration for S3 bucket. The AppS3Bucket configuration must be set to a S3 bucket.");
            //}

            //logger.LogInformation($"Configured to use bucket {this.BucketName}");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EnergyReadingGroup>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get([FromQuery, Required, Range(1,100)] byte percvalue)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok( _queries.FindDivergingReadings(percvalue));
        }
    }
}
