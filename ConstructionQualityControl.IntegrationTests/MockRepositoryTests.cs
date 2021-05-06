using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain.Mocks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionQualityControl.ModuleTests
{
    public class MockRepositoryTests
    {
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
        }

        [Test]
        public async Task Positive_Add_Is_Incremented_Entities()
        {
            var expected = new City { Id = 4, Name = "ABC" };

            await repo.AddAsync(expected);

            Assert.Contains(expected, repo.Data);
        }

        [Test]
        public async Task Positive_DeleteById_Is_Deleting_Entity()
        {
            await repo.DeleteByIdAsync(3);

            Assert.AreEqual(2, repo.Data.Count);
            Assert.AreEqual("BBB", repo.Data[0].Name);
            Assert.AreEqual("ZZZ", repo.Data[1].Name);
        }

        [Test]
        public async Task Positive_GetById_Is_Getting_Correct_Entity()
        {
            var actual = await repo.GetByIdAsync(2);

            Assert.AreEqual(repo.Data[1], actual);
        }

        [Test]
        public async Task Positive_GetById_Is_Getting_Null_When_Entity_Not_Exist()
        {
            var actual = await repo.GetByIdAsync(4);

            Assert.AreEqual(null, actual);
        }

        [Test]
        public void Positive_Update()
        {
            var expected = new City { Id = 2, Name = "123" };

            repo.Update(expected);

            Assert.AreEqual(expected, repo.Data[1]);
        }

        [Test]
        public async Task Positive_Get_Without_Parameters_Get_All_Entities()
        {
            var actual = await repo.GetAsync();

            Assert.AreEqual(3, actual.Count());
            Assert.AreEqual("BBB", actual.ElementAt(0).Name);
            Assert.AreEqual("ZZZ", actual.ElementAt(1).Name);
            Assert.AreEqual("AAA", actual.ElementAt(2).Name);
        }

        [Test]
        public async Task Positive_Get_Expression()
        {
            var actual = await repo.GetAsync(c => c.Name == "ZZZ");

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual("ZZZ", actual.ElementAt(0).Name);
        }

        [Test]
        public async Task Positive_Get_Filter()
        {
            var actual = await repo.GetAsync(orderBy: c => c.OrderBy(n => n.Name));

            Assert.AreEqual(3, actual.Count());
            Assert.AreEqual("AAA", actual.ElementAt(0).Name);
            Assert.AreEqual("BBB", actual.ElementAt(1).Name);
            Assert.AreEqual("ZZZ", actual.ElementAt(2).Name);
        }

        [Test]
        public async Task Positive_Get_Expression_And_Filter()
        {
            var actual = await repo.GetAsync(c => c.Name != "ZZZ", c => c.OrderByDescending(n => n.Name));

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual("BBB", actual.ElementAt(0).Name);
            Assert.AreEqual("AAA", actual.ElementAt(1).Name);
        }
    }
}