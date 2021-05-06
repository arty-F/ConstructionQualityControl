using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Dtos;
using ConstructionQualityControl.Domain.MapperProfile;
using ConstructionQualityControl.Web.Authentication;
using ConstructionQualityControl.Web.Handlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConstructionQualityControl.UnitTests
{
    class OrderHandlerTests
    {
        MockUnitOfWork mockUoW;
        MockRepository<Order> orderRepo;
        MockRepository<City> cityRepo;
        MockRepository<WorkOffer> workOfferRepo;
        MockRepository<User> userRepo;
        OrderHandler handler;
        IMapper mapper;
        List<Order> orders;
        List<City> cities;
        List<User> users;
        List<WorkOffer> workOffers;

        List<Claim> customerClaims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier.ToString(), 0.ToString()),
                new Claim(ClaimTypes.Role.ToString(), RolesManager.Customer),
            };

        List<Claim> builderClaims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier.ToString(), 1.ToString()),
                new Claim(ClaimTypes.Role.ToString(), RolesManager.Builder),
            };

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            mapper = config.CreateMapper();

            var region = new Region { Id = 0, Name = "", Cities = new List<City>() };
            var city = new City { Id = 0, Name = "", Region = region, Latitude = 0, Longitude = 0 };
            region.Cities.Add(city);
            cities = new List<City> { city };

            users = new List<User>
            {
                new User {Id = 0, City = city, Login = RolesManager.Customer, Password = "", Role = RolesManager.Customer },
                new User {Id = 1, City = city, Login = RolesManager.Builder, Password = "", Role = RolesManager.Builder },
                new User {Id = 2, City = city, Login = RolesManager.Customer, Password = "", Role = RolesManager.Customer },
            };

            workOffers = new List<WorkOffer>
            {
                new WorkOffer { Id = 0, Worker = users[1], Date = DateTime.Now, Message = "" }
            };

            orders = new List<Order>
            {
                new Order { Id = 0, City = city, IsRoot = true, WorkOffers = workOffers, User = users[0], CreationDate = DateTime.Now, PrePaid = 0, PostPaid = 0  },
                new Order { Id = 1, City = city, IsRoot = true, IsStarted = true, WorkOffers = workOffers, User = users[2], CreationDate = DateTime.Now, PrePaid = 0, PostPaid = 0  }
            };

            cityRepo = new MockRepository<City>(cities);
            orderRepo = new MockRepository<Order>(orders);
            userRepo = new MockRepository<User>(users);
            workOfferRepo = new MockRepository<WorkOffer>(workOffers);
            mockUoW = new MockUnitOfWork(cityRep: cityRepo, regionRep: null, userRep: userRepo,
                orderRep: orderRepo, commentRep: null, reportRep: null, workOfferRep: workOfferRepo);
            handler = new OrderHandler(mockUoW, mapper);
        }

        [Test]
        public void Positive_CreateOrder_Not_Throw_Exception()
        {
            OrderCreateDto order = new OrderCreateDto
            {
                City = mapper.Map<CityReadDto>(cities[0]),
                IsRoot = true,
                User = mapper.Map<UserReadDto>(users[0]),
                Demands = "",
                PostPaid = 0,
                PrePaid = 0
            };

            Assert.DoesNotThrowAsync(async () => await handler.CreateOrderAsync(order));
        }

        [Test]
        public void Negative_CreateOrder_With_No_Data_Throw_Exception()
        {
            OrderCreateDto order = new OrderCreateDto
            {
                City = mapper.Map<CityReadDto>(cities[0]),
                IsRoot = true,
                Demands = "",
                PostPaid = 0,
                PrePaid = 0
            };

            Assert.ThrowsAsync<NullReferenceException>(async () => await handler.CreateOrderAsync(order));
        }

        [Test]
        public async Task After_CreateOrder_Orders_Count_Incremented()
        {
            var result = await orderRepo.GetAsync();
            var expected = result.Count() + 1;

            OrderCreateDto order = new OrderCreateDto
            {
                City = mapper.Map<CityReadDto>(cities[0]),
                IsRoot = true,
                User = mapper.Map<UserReadDto>(users[0]),
                Demands = "",
                PostPaid = 0,
                PrePaid = 0
            };

            await handler.CreateOrderAsync(order);
            result = await orderRepo.GetAsync();
            var actual = result.Count();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Positive_GetOrder()
        {
            var expected = orders.First().CreationDate;

            var result = await handler.GetOrderAsync(0, customerClaims);
            var actual = result.CreationDate;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Negative_GetOrder_Wrong_Order_Id()
        {
            Assert.ThrowsAsync<Exception>(async () => await handler.GetOrderAsync(2, customerClaims));
        }

        [Test]
        public void Negative_GetOrder_Wrong_Claims()
        {
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await handler.GetOrderAsync(1, customerClaims));
        }

        [Test]
        public async Task Positive_GetOrders_Returns_One_Order_For_Customer()
        {
            var expected = 1;

            var result = await handler.GetOrdersAsync(customerClaims);
            var actual = result.Count();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Positive_GetOrders_Returns_One_Order_For_Builder()
        {
            var expected = 1;

            var result = await handler.GetOrdersAsync(builderClaims);
            var actual = result.Count();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Positive_GetWorksByCityID_Returns_One_Order()
        {
            var expected = 1;

            var result = await handler.GetWorksByCityIdAsync(0);
            var actual = result.Count();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Negative_GetWorksByCityID_Returns_Empty()
        {
            var expected = 0;

            var result = await handler.GetWorksByCityIdAsync(1);
            var actual = result.Count();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Positive_GetWorks_Returns_All_Unstarted_Works()
        {
            var expected = 1;

            var result = await handler.GetWorksAsync();
            var actual = result.Count();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Positive_GetWorkByIdAsync_Returns_Correct_Work()
        {
            var expected = orders[0].CreationDate;

            var result = await handler.GetWorkByIdAsync(orders[0].Id);
            var actual = result.CreationDate;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Negative_GetWorkByIdAsync_Returns_Null()
        {
            var actual = await handler.GetWorkByIdAsync(2);

            Assert.IsNull(actual);
        }

        [Test]
        public async Task Positive_AddOffer_Is_Incrementing_Offers()
        {
            var result = await orderRepo.GetByIdAsync(0);
            var expected = result.WorkOffers.Count + 1;

            var offer = new WorkOfferCreateDto
            {
                Worker = mapper.Map<UserReadDto>(users[1])
            };
            await handler.AddOfferAsync(0, offer);
            result = await orderRepo.GetByIdAsync(0);
            var actual = result.WorkOffers.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Negative_AddOffer_Cant_Add_To_Started_Order()
        {
            var offer = new WorkOfferCreateDto
            {
                Worker = mapper.Map<UserReadDto>(users[1])
            };

            Assert.ThrowsAsync<Exception>(async () => await handler.AddOfferAsync(1, offer));
        }
    }
}
