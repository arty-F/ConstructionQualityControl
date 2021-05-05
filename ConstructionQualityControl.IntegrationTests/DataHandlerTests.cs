using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Dtos;
using ConstructionQualityControl.Domain.MapperProfile;
using ConstructionQualityControl.Domain.Mocks;
using ConstructionQualityControl.Web.Authentication;
using ConstructionQualityControl.Web.Handlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConstructionQualityControl.IntegrationTests
{
    class DataHandlerTests
    {
        MockUnitOfWork mockUoW;
        MockRepository<Order> orderRepo;
        MockRepository<City> cityRepo;
        MockRepository<WorkOffer> workOfferRepo;
        MockRepository<User> userRepo;
        MockRepository<Comment> commentRepo;
        MockRepository<Report> reportRepo;
        DataHandler handler;
        IMapper mapper;
        List<Order> orders;
        List<City> cities;
        List<User> users;
        List<WorkOffer> workOffers;
        List<Comment> comments;
        List<Report> reports;

        OrderReadDto orderDto;
        UserReadDto userDto;

        [SetUp]
        public async Task Setup()
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
            };

            workOffers = new List<WorkOffer>
            {
                new WorkOffer { Id = 0, Worker = users[1], Date = DateTime.Now, Message = "" }
            };

            orders = new List<Order>
            {
                new Order { Id = 0, City = city, IsRoot = true, IsStarted = true, WorkOffers = workOffers, User = users[0], CreationDate = DateTime.Now, PrePaid = 0, PostPaid = 0  }
            };

            comments = new List<Comment>
            {
                new Comment { Id = 0, Date = DateTime.Now, Order = orders[0], User = users[0], Text = "text"}
            };

            reports = new List<Report>()
            {
                new Report { Id = 0, Data = new byte[0], CreationDate = DateTime.Now, Order = orders[0], User = users[1]}
            };

            orders[0].Comments = comments;
            orders[0].Reports = reports;

            cityRepo = new MockRepository<City>(cities);
            orderRepo = new MockRepository<Order>(orders);
            userRepo = new MockRepository<User>(users);
            workOfferRepo = new MockRepository<WorkOffer>(workOffers);
            commentRepo = new MockRepository<Comment>(comments);
            reportRepo = new MockRepository<Report>(reports);
            mockUoW = new MockUnitOfWork(cityRep: cityRepo, regionRep: null, userRep: userRepo,
                orderRep: orderRepo, commentRep: commentRepo, reportRep: reportRepo, workOfferRep: workOfferRepo);
            handler = new DataHandler(mockUoW, mapper);

            var order = await mockUoW.GetRepository<Order>().GetByIdAsync(0);
            orderDto = mapper.Map<OrderReadDto>(order);
            var user = await mockUoW.GetRepository<User>().GetByIdAsync(0);
            userDto = mapper.Map<UserReadDto>(user);
        }

        [Test]
        public void Positive_AddComment_Not_Throw_Exception()
        {
            CommentCreateDto commentDto = new CommentCreateDto
            {
                Order = orderDto,
                Text = " ",
                User = userDto
            };

            Assert.DoesNotThrowAsync(async () => await handler.AddCommentAsync(0, commentDto));
        }

        [Test]
        public async Task Positive_AddComment_Incremented_Comments()
        {
            var order = await mockUoW.GetRepository<Order>().GetByIdAsync(0);
            var expected = order.Comments.Count + 1;
            CommentCreateDto commentDto = new CommentCreateDto
            {
                Order = orderDto,
                Text = " ",
                User = userDto
            };

            await handler.AddCommentAsync(0, commentDto);
            var actual = order.Comments.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Negative_AddComment_With_No_Text_Throw_Exception()
        {
            CommentCreateDto commentDto = new CommentCreateDto
            {
                Order = orderDto,
                Text = "",
                User = userDto
            };

            Assert.ThrowsAsync<Exception>(async () => await handler.AddCommentAsync(0, commentDto));
        }

        [Test]
        public void Negative_AddComment_With_No_User_Throw_Exception()
        {
            CommentCreateDto commentDto = new CommentCreateDto
            {
                Order = orderDto,
                Text = " "
            };

            Assert.ThrowsAsync<NullReferenceException>(async () => await handler.AddCommentAsync(0, commentDto));
        }

        [Test]
        public void Positive_AddReports_Not_Throw_Exception()
        {
            ReportCreateDto[] reportsDto = new ReportCreateDto[1];
            reportsDto[0] = new ReportCreateDto
            {
                Data = "0",
                Order = orderDto,
                User = userDto
            };

            Assert.DoesNotThrowAsync(async () => await handler.AddReportsAsync(0, reportsDto));
        }

        [Test]
        public async Task Positive_AddReports_Incremented_Reports()
        {
            var order = await mockUoW.GetRepository<Order>().GetByIdAsync(0);
            var expected = order.Reports.Count + 2;

            ReportCreateDto[] reportsDto = new ReportCreateDto[2];
            reportsDto[0] = new ReportCreateDto
            {
                Data = "0",
                Order = orderDto,
                User = userDto
            };
            reportsDto[1] = new ReportCreateDto
            {
                Data = "1",
                Order = orderDto,
                User = userDto
            };

            await handler.AddReportsAsync(0, reportsDto);
            var actual = order.Reports.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Negative_AddReports_Empty_Array_Throw_Exception()
        {
            ReportCreateDto[] reportsDto = new ReportCreateDto[2];

            Assert.ThrowsAsync<NullReferenceException>(async () => await handler.AddReportsAsync(0, reportsDto));
        }

        [Test]
        public void Negative_AddReports_With_No_User_Throw_Exception()
        {
            ReportCreateDto[] reportsDto = new ReportCreateDto[1];
            reportsDto[0] = new ReportCreateDto
            {
                Data = "0",
                Order = orderDto
            };

            Assert.ThrowsAsync<NullReferenceException>(async () => await handler.AddReportsAsync(0, reportsDto));
        }
    }
}
