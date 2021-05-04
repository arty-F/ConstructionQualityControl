using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Dtos;
using System.Text;

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
            CreateMap<User, UserCreateDto>();
            CreateMap<UserReadDto, User>();
            CreateMap<User, UserReadDto>();
            
            CreateMap<WorkOfferCreateDto, WorkOffer>();
            CreateMap<WorkOffer, WorkOfferReadDto>();

            CreateMap<Order, OrderRootReadDto>();
            CreateMap<Order, WorkReadDto>();
            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderReadDto, Order>();

            CreateMap<Comment, CommentReadDto>();
            CreateMap<CommentReadDto, Comment>();
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<CommentCreateDto, Comment>();

            CreateMap<Report, ReportReadDto>();
            CreateMap<ReportReadDto, Report>();
            CreateMap<ReportCreateDto, Report>();

            CreateMap<string, byte[]>().ConvertUsing(s => Encoding.ASCII.GetBytes(s));
            CreateMap<byte[], string>().ConvertUsing(b => Encoding.ASCII.GetString(b));
        }
    }
}
