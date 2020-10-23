using ConstructionQualityControl.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionQualityControl.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QualityControlContext context;

        public UnitOfWork(QualityControlContext context) => this.context = context;

        public IRepository<IEntity> GetRepository<T>() where T : IEntity
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
