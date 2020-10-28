using ConstructionQualityControl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Data.Mocks
{
    public class MockRepository<T> : IRepository<T> where T : class, IEntity
    {
        public List<T> Data { get; set; }

        public MockRepository()
        {
            Data = new List<T>();
        }

        public MockRepository(IEnumerable<T> data)
        {
            Data = data.ToList();
        }

        public async Task AddAsync(T entity)
        {
            await Task.Run(() => Data.Add(entity));
        }

        public async Task DeleteByIdAsync(int id)
        {
            var finded = await Task.Run(() => FindByIdAsync(id));
            if (finded != null)
                await Task.Run(() => Data.Remove(finded));
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = Data.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return await Task.Run( () => orderBy(query).ToList());
            else
                return await Task.Run( () => query.ToList());
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var finded = await Task.Run(() => FindByIdAsync(id));
            return finded;
        }

        public void Update(T entity)
        {
            var finded = FindByIdAsync(entity.Id);
            if (finded != null)
                Data[Data.IndexOf(finded)] = entity;
        }

        private T FindByIdAsync(int id)
        {
            return Data.FirstOrDefault(d => d.Id == id);
        }
    }
}
