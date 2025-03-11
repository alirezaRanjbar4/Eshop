using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ServicePrice
{
    public interface IServicePriceService : IBaseService<ServicePriceEntity>, IScopedDependency
    {
    }
}
