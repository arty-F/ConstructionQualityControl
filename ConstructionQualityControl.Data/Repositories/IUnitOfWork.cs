using ConstructionQualityControl.Data.Models;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Data.Repositories
{
    public interface IUnitOfWork
    {
        public IRepository<T> GetRepository<T>() where T : class, IEntity;
        public Task SaveAsync();
    }
}
