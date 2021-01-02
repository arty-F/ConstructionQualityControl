using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Dtos;
using System.Collections.Generic;

namespace ConstructionQualityControl.Domain.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegionCreateDto, Region>();
            CreateMap<Region, RegionReadDto>();
            CreateMap<RegionReadDto, Region>();

            CreateMap<CityCreateDto, City>();
            CreateMap<City, CityReadDto>();
            CreateMap<CityReadDto, City>();

            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReadDto>();
            CreateMap<UserReadDto, User>();
        }
    }
}
