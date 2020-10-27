using ConstructionQualityControl.Data;
using ConstructionQualityControl.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionQualityControl.ModuleTests
{
    public class MSSQLRepositoryTests
    {
        Mock<DbSet<City>> mockSet;
        Mock<QualityControlContext> mockContext;
        MSSQLRepository<City> repo;

        [SetUp]
        public void Setup()
        {
            var data = new List<City>
            {
                new City { Name = "BBB" },
                new City { Name = "ZZZ" },
                new City { Name = "AAA" },
            }.AsQueryable();

            mockSet = new Mock<DbSet<City>>();
            mockSet.As<IQueryable<City>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<City>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<City>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<City>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<QualityControlContext>();
            mockContext.Setup(c => c.Cities).Returns(mockSet.Object);

            repo = new MSSQLRepository<City>(mockContext.Object);
        }
         
        [Test]
        public async Task Add_is_adding_entity_to_DbSet()
        {
            var expName = "XXX";
            await repo.AddAsync(new City { Id = 10, Name = expName });

            var actual = mockSet.Object.FirstOrDefault(c => c.Name == expName);
            Assert.AreEqual(expName, actual.Name);
        }
    }
}
