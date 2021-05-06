using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using ConstructionQualityControl.Domain.MapperProfile;
using ConstructionQualityControl.Web.Authentication;
using ConstructionQualityControl.Web.Handlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionQualityControl.UnitTests
{
    class AuthenticationHandlerTests
    {
        MockUnitOfWork mockUoW;
        MockRepository<User> repo;
        AuthenticationHandler handler;
        IMapper mapper;
        Cryptographer cryptographer;
        List<User> data;

        const string login = "gg@gg.gg";
        const string password = "12345a";
        UserReadDto userDto;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            mapper = config.CreateMapper();
            cryptographer = new Cryptographer("secret key");

            data = new List<User>
            {
                new User { Id = 1, Login= login, Password = cryptographer.Encypt(password), Role = "Customer" }
            };

            repo = new MockRepository<User>(data);
            mockUoW = new MockUnitOfWork(userRep: repo);
            handler = new AuthenticationHandler(mockUoW, mapper, cryptographer);
            userDto = mapper.Map<UserReadDto>(data.First());
            JWTAuthenticationManager.Initialize();
        }

        [Test]
        public async Task Positive_Login()
        {
            var expected = JWTAuthenticationManager.GetToken(userDto);

            var actual = await handler.LoginAsync(login, password);

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [Test]
        public void Negative_Login_Wrong_Password()
        {
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await handler.LoginAsync(login, "pas"));
        }

        [Test]
        public void Negative_Login_Wrong_Login()
        {
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await handler.LoginAsync("log", password));
        }

        [Test]
        public void Negative_Login_Wrong_Login_And_Password()
        {
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await handler.LoginAsync("log", "pas"));
        }

        [Test]
        public async Task Positive_Get_User_Data()
        {
            var expected = userDto;

            var actual = await handler.GetCurrentUserDataAsync(login);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Login, actual.Login);
        }

        [Test]
        public async Task Negative_Get_User_Data_Null()
        {
            var actual = await handler.GetCurrentUserDataAsync("log");

            Assert.IsNull(actual);
        }
    }
}
