using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using ConstructionQualityControl.Web.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityHandler handler;

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            handler = new CityHandler(unitOfWork, mapper);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityReadDto>>> GetAllCities()
        {
            try
            {
                return Ok(await handler.GetAllCitiesAsync());
            }
            catch (NullReferenceException) { return NotFound(); }
            catch (Exception) { return BadRequest(); }
        }
    }
}
