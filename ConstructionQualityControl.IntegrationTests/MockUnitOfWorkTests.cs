using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Mocks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionQualityControl.ModuleTests
{
    public class MockUnitOfWorkTests
    {
        MockUnitOfWork mockUoW;
        MockRepository<City> repo;

        [SetUp]
        public void Setup()
        {
            var data = new List<City>
            {
                new City { Id = 1, Name = "BBB" },
                new City { Id = 2, Name = "ZZZ" },
                new City { Id = 3, Name = "AAA" },
            };

            repo = new MockRepository<City>(data);

            mockUoW = new MockUnitOfWork(cityRep: repo);
        }

        [Test]
        public void GetRepository_is_getting_correct_repository()
        {
            var expected = repo;

            var actual = mockUoW.GetRepository<City>();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task UoW_works_with_the_same_repository()
        {
            var repo = mockUoW.GetRepository<City>();
            var expected = new City { Id = 4, Name = "123" };

            await repo.AddAsync(expected);
            var actual = await repo.GetByIdAsync(4);

            Assert.AreEqual(expected, actual);
        }
    }
}
