using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Dtos;

namespace ConstructionQualityControl.Domain.MapperProfile
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<RegionCreateDto, Region>();
            CreateMap<Region, RegionReadDto>();
            CreateMap<RegionReadDto, Region>();

            CreateMap<CityCreateDto, City>();
            CreateMap<City, CityReadDto>();
        }
    }
}
