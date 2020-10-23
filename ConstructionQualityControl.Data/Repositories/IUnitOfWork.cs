using ConstructionQualityControl.Data.Models;

namespace ConstructionQualityControl.Data.Repositories
{
    public interface IUnitOfWork
    {
        public IRepository<IEntity> GetRepository<T>() where T : IEntity;
        public void Save();
    }
}
