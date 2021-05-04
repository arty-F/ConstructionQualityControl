using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using ConstructionQualityControl.Domain.MapperProfile;
using ConstructionQualityControl.Domain.Mocks;
using ConstructionQualityControl.Web.Authentication;
using ConstructionQualityControl.Web.Handlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionQualityControl.IntegrationTests
{
    class UserHandlerTests
    {
        MockUnitOfWork mockUoW;
        MockRepository<User> repo;
        MockRepository<City> cityRepo;
        UserHandler handler;
        IMapper mapper;
        Cryptographer cryptographer;
        List<User> data;
        List<City> cities;
        UserCreateDto userDto;

        const string login = "gg@gg.gg";
        const string password = "12345a";

        [SetUp]
        public async Task Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            mapper = config.CreateMapper();
            cryptographer = new Cryptographer();

            data = new List<User>
            {
                new User { Login= login, Password = password }
            };

            cities = new List<City>
            {
                new City {Id = 0, Name = ""}
            };

            repo = new MockRepository<User>(data);
            cityRepo = new MockRepository<City>(cities);
            await cityRepo.AddAsync(new City { Id = 0, Name = "" });
            mockUoW = new MockUnitOfWork(userRep: repo, cityRep: cityRepo);
            handler = new UserHandler(mockUoW, mapper, cryptographer);
            userDto = mapper.Map<UserCreateDto>(data.First());
        }

        [Test]
        public void Positive_Create_New_User()
        {
            var user = new User { Login = "newUser", Password = password, City = new City { Id = 0, Name = ""} };
            var userCreateDto = mapper.Map<UserCreateDto>(user);

            Assert.DoesNotThrowAsync(async () => await handler.CreateUserAsync(userCreateDto));
        }
    }
}
