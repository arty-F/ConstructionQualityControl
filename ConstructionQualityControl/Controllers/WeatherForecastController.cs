using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConstructionQualityControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        IUnitOfWork uow;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            this.uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> Get()
        {
            var result = await uow.GetRepository<City>().GetAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<City>> Post(City city)
        {
            var repo = uow.GetRepository<City>();
            await repo.AddAsync(city);
            await uow.SaveAsync();

            return Ok();
            //return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }
    }
}
