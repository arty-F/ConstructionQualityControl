﻿using ConstructionQualityControl.Data;
using ConstructionQualityControl.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Domain
{
    public class MSSQLRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly QualityControlContext context;
        private readonly DbSet<T> dbSet;

        public MSSQLRepository(QualityControlContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);

        public async Task DeleteByIdAsync(int id)
        {
            T entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);

            dbSet.Remove(entityToDelete);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return await Task.Run(() => orderBy(query).ToList());
            else
                return await Task.Run(() => query.ToList());
        }

        public async Task<T> GetByIdAsync(int id) => await dbSet.FindAsync(id);

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}