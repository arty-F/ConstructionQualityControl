using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Dtos;

namespace ConstructionQualityControl.Domain.MapperProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, ReadCityDto>();
        }
    }
}
