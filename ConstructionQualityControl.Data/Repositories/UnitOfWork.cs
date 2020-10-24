using ConstructionQualityControl.Data.Models;
using System;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly QualityControlContext context;

        public UnitOfWork(QualityControlContext context) => this.context = context;

        public IRepository<T> GetRepository<T>() where T : class, IEntity => new Repository<T>(context);

        public async Task SaveAsync() => await context.SaveChangesAsync();

        #region IDisposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    context.Dispose();

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork() => Dispose(false);
        #endregion
    }
}
