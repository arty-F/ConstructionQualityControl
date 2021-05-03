using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.MapperProfile;
using ConstructionQualityControl.Domain.Mocks;
using ConstructionQualityControl.Web.Handlers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionQualityControl.IntegrationTests
{
    class CityHandlerTests
    {
        MockUnitOfWork mockUoW;
        MockRepository<City> repo;
        CityHandler handler;
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
            handler = new CityHandler(mockUoW, mapper);
        }

        [Test]
        public async Task Geting_correct_count()
        {
            var expected = data.Count;

            var result = await handler.GetAllCitiesAsync();
            var actual = result.Count();

            Assert.AreEqual(expected, actual);
        }
    }
}
