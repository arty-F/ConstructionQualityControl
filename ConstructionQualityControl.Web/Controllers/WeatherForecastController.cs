using System.Collections.Generic;
using System.Threading.Tasks;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConstructionQualityControl.Controllers.Web
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> Get()
        {
            //var result = await unitOfWork.Get();
            //var result = await uow.GetRepository<City>().GetAsync();
            
            return Ok();
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
