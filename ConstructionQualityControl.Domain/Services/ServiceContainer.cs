using ConstructionQualityControl.Data;
using ConstructionQualityControl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Domain.Services
{
    public class ServiceContainer : IServiceContainer
    {
        IUnitOfWork uow;

        public ServiceContainer(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<string> Get()
        {
            var result = await uow.GetRepository<City>().GetAsync();
            return result.Count().ToString();
        }
    }
}
