using System.Threading.Tasks;

namespace ConstructionQualityControl.Domain.Services
{
    public interface IServiceContainer
    {
        public Task<string> Get();
    }
}
