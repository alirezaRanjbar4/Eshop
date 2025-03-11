using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Service
{
    public interface IServiceRepository : IBaseRepository<ServiceEntity>, IScopedDependency
    {
    }
}
