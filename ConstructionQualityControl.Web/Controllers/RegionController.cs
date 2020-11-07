using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RegionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionReadDto>>> GetAllRegions()
        {
            var regions = await unitOfWork.GetRepository<Region>().GetAsync();

            if (regions == null)
                return NotFound();

            return Ok(mapper.Map<IEnumerable<RegionReadDto>>(regions));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegionReadDto>> GetRegionById(int id)
        {
            var region = await unitOfWork.GetRepository<Region>().GetByIdAsync(id);

            if (region == null)
                return NotFound();

            return Ok(mapper.Map<RegionReadDto>(region));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion(RegionCreateDto regionDto)
        {
            await unitOfWork.GetRepository<Region>().AddAsync(mapper.Map<Region>(regionDto));
            await unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRegion(RegionReadDto regionDto)
        {
            unitOfWork.GetRepository<Region>().Update(mapper.Map<Region>(regionDto));
            await unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            await unitOfWork.GetRepository<Region>().DeleteByIdAsync(id);
            await unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
