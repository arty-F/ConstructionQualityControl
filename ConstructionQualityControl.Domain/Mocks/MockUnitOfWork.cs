using ConstructionQualityControl.Data.Models;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Domain.Mocks
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public MockRepository<City> CityRep { get; private set; }
        public MockRepository<Region> RegionRep { get; private set; }
        public MockRepository<Customer> CustomerRep { get; private set; }
        public MockRepository<User> UserRep { get; private set; }
        public MockRepository<Builder> BuilderRep { get; private set; }
        public MockRepository<Order> OrderRep { get; private set; }
        public MockRepository<Comment> CommentRep { get; private set; }
        public MockRepository<Report> ReportRep { get; private set; }

        public MockUnitOfWork(MockRepository<City> cityRep = null, MockRepository<Region> regionRep = null, 
            MockRepository<Customer> customerRep = null, MockRepository<Builder> builderRep = null,
            MockRepository<User> userRep = null, MockRepository<Order> orderRep = null, 
            MockRepository<Comment> commentRep = null, MockRepository<Report> reportRep = null)
        {
            CityRep = cityRep;
            RegionRep = regionRep;
            CustomerRep = customerRep;
            UserRep = userRep;
            BuilderRep = builderRep;
            OrderRep = orderRep;
            CommentRep = commentRep;
            ReportRep = reportRep;
        }

        public async Task SaveAsync()
        {
            await Task.Run(() => null);
        }

        public IRepository<T> GetRepository<T>() where T : class, IEntity
        {
            if (typeof(T) == typeof(City))
                return CityRep as MockRepository<T>;

            if (typeof(T) == typeof(Region))
                return RegionRep as MockRepository<T>;

            if (typeof(T) == typeof(Customer))
                return CustomerRep as MockRepository<T>;

            if (typeof(T) == typeof(Builder))
                return BuilderRep as MockRepository<T>;

            if (typeof(T) == typeof(User))
                return UserRep as MockRepository<T>;

            if (typeof(T) == typeof(Order))
                return OrderRep as MockRepository<T>;

            if (typeof(T) == typeof(Comment))
                return CommentRep as MockRepository<T>;

            if (typeof(T) == typeof(Report))
                return ReportRep as MockRepository<T>;

            return null;
        }
    }
}
