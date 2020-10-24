using ConstructionQualityControl.Data.Models;

namespace ConstructionQualityControl.Data.Repositories
{
    public interface IUnitOfWork
    {
        public IRepository<T> GetRepository<T>() where T : class, IEntity;
        public void SaveAsync();
    }
}
