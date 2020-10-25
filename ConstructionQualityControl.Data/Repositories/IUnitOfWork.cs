using ConstructionQualityControl.Data.Models;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Data.Repositories
{
    /// <summary>
    /// Provides access to entities repositories and guarantees the use the same DbConext.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Return specific by T IRepository. T is must be IEntity implementation.
        /// </summary>
        /// <typeparam name="T">IEntity implementation.</typeparam>
        /// <returns>Repository of T entity type.</returns>
        public IRepository<T> GetRepository<T>() where T : class, IEntity;

        /// <summary>
        /// Save changes to the used DbContext.
        /// </summary>
        public Task SaveAsync();
    }
}
