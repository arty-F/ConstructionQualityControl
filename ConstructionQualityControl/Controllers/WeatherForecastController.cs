using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionQualityControl.Data;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConstructionQualityControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        IServiceContainer services;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IServiceContainer services)
        {
            _logger = logger;
            this.services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> Get()
        {
            var result = await services.Get();
            //var result = await uow.GetRepository<City>().GetAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<City>> Post(City city)
        {
            //var repo = uow.GetRepository<City>();
            //await repo.AddAsync(city);
            //await uow.SaveAsync();

            return Ok();
            //return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }
    }
}
