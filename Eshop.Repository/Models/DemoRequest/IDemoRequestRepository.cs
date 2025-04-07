using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.DemoRequest
{
    public interface IDemoRequestRepository : IBaseRepository<DemoRequestEntity>, IScopedDependency
    {
    }
}
