using ConstructionQualityControl.Data.Models;
using System;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly QualityControlContext context;

        public UnitOfWork(QualityControlContext context) => this.context = context;

        public IRepository<T> GetRepository<T>() where T : class, IEntity => new MSSQLRepository<T>(context);

        public async Task SaveAsync() => await context.SaveChangesAsync();

        #region IDisposable
        private bool disposed = false;

        protected void DisposeContext()
        {
            if (disposed) return;                    

            disposed = true;
            context.Dispose();
        }

        public void Dispose()
        {
            DisposeContext();
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork() => DisposeContext();
        #endregion
    }
}
