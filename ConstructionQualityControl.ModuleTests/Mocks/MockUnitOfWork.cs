using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using System.Threading.Tasks;

namespace ConstructionQualityControl.UnitTests
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public MockRepository<City> CityRep { get; private set; }
        public MockRepository<Region> RegionRep { get; private set; }
        public MockRepository<User> UserRep { get; private set; }
        public MockRepository<Order> OrderRep { get; private set; }
        public MockRepository<Comment> CommentRep { get; private set; }
        public MockRepository<Report> ReportRep { get; private set; }
        public MockRepository<WorkOffer> WorkOfferRep { get; private set; }

        public MockUnitOfWork(MockRepository<City> cityRep = null, MockRepository<Region> regionRep = null, 
            MockRepository<User> userRep = null, MockRepository<Order> orderRep = null, 
            MockRepository<Comment> commentRep = null, MockRepository<Report> reportRep = null, MockRepository<WorkOffer> workOfferRep = null)
        {
            CityRep = cityRep;
            RegionRep = regionRep;
            UserRep = userRep;
            OrderRep = orderRep;
            CommentRep = commentRep;
            ReportRep = reportRep;
            WorkOfferRep = workOfferRep;
        }

        public async Task SaveAsync()
        {
            await Task.Run(() => 0);
        }

        public IRepository<T> GetRepository<T>() where T : class, IEntity
        {
            if (typeof(T) == typeof(City))
                return CityRep as MockRepository<T>;

            if (typeof(T) == typeof(Region))
                return RegionRep as MockRepository<T>;

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
