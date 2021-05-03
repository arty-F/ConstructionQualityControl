using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Web.Handlers
{
    public class CityHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CityReadDto>> GetAllCitiesAsync()
        {
            var cities = await unitOfWork.GetRepository<City>().GetAsync();
            if (cities == null) throw new NullReferenceException();
            return mapper.Map<IEnumerable<CityReadDto>>(cities);
        }
    }
}
