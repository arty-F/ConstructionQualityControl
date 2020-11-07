using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityReadDto>>> GetAllCities()
        {
            var cities = await unitOfWork.GetRepository<City>().GetAsync();

            if (cities == null)
                return NotFound();

            var a = mapper.Map<IEnumerable<CityReadDto>>(cities);
            //Region is null!!!
            //TODO add city to Region cities list
            throw new Exception();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(CityCreateDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);
            city.Region = await unitOfWork.GetRepository<Region>().GetByIdAsync(city.Region.Id);
            await unitOfWork.GetRepository<City>().AddAsync(city);
            await unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
