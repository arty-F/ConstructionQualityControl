using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionQualityControl.Data;
using ConstructionQualityControl.Data.Repositories.Implementation;
using ConstructionQualityControl.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConstructionQualityControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        ITestRep rep;




        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITestRep rep)
        {
            _logger = logger;
            this.rep = rep;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            rep.Get();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
