﻿using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Dtos;

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

            CreateMap<WorkOfferCreateDto, WorkOffer>();
            CreateMap<WorkOffer, WorkOfferReadDto>();

            CreateMap<Order, OrderRootReadDto>();
            CreateMap<Order, WorkReadDto>();
        }
    }
}
