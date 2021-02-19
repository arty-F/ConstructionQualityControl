using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Dtos;
using ConstructionQualityControl.Domain.MapperProfile;
using ConstructionQualityControl.Domain.Mocks;
using ConstructionQualityControl.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace ConstructionQualityControl.IntegrationTests
{
    public class CityControllerTests
    {
        MockUnitOfWork mockUoW;
        MockRepository<City> repo;
        CityController controller;
        IMapper mapper;
        List<City> data;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            mapper = config.CreateMapper();

            data = new List<City>
            {
                new City { Id = 1, Name = "BBB" },
                new City { Id = 2, Name = "ZZZ" },
                new City { Id = 3, Name = "AAA" },
            };

            repo = new MockRepository<City>(data);
            mockUoW = new MockUnitOfWork(cityRep: repo);
            controller = new CityController(mockUoW, mapper);
        }


        [Test]
        public async Task Geting_correct_count()
        {
            var expected = data.Count;

            var result = await controller.GetAllCities();
            var okResult = result.Result as OkObjectResult;
            var actual = (okResult.Value as IEnumerable<CityReadDto>).Count();

            Assert.AreEqual(expected, actual);
        }
    }
}
