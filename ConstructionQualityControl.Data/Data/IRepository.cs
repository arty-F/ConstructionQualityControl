using ConstructionQualityControl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Data
{
    /// <summary>
    /// Generic repository which provides async access to DbContext.
    /// </summary>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Returns an entities which meet requirements of the "filter" lambda expression parameter and ordered by "orderBy" predicate parameter.
        /// </summary>
        /// <param name="filter">Lambda expression.</param>
        /// <param name="orderBy">Sort order.</param>
        /// <returns>IEnumerable of finded enities or null.</returns>
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// Return an entity with the given id value.
        /// </summary>
        /// <param name="id">Primary key of entity.</param>
        /// <returns>Finded entity or null.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Adds the current entity to watch by DbContext.
        /// </summary>
        /// <param name="entity">Entity to adding.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Update current entity in to DbContext.
        /// </summary>
        /// <param name="entity">Entity to updating.</param>
        void Update(T entity);

        /// <summary>
        /// Delete current entity from DbContext.
        /// </summary>
        /// <param name="id">Entity to remove.</param>
        Task DeleteAsync(int id);
    }
}
