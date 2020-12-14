using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
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
            if (cities == null) return NotFound();
            return Ok(mapper.Map<IEnumerable<CityReadDto>>(cities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityReadDto>> GetCityById(int id)
        {
            var city = await unitOfWork.GetRepository<City>().GetByIdAsync(id);
            if (city == null) return NotFound();
            return Ok(mapper.Map<CityReadDto>(city));
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

        [HttpPut]
        public async Task<IActionResult> UpdateCity(CityReadDto cityDto)
        {
            unitOfWork.GetRepository<City>().Update(mapper.Map<City>(cityDto));
            await unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCityById(int id)
        {
            await unitOfWork.GetRepository<City>().DeleteByIdAsync(id);
            await unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
