using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.DemoRequest
{
    public interface IDemoRequestService : IBaseService<DemoRequestEntity>, IScopedDependency
    {
    }
}
