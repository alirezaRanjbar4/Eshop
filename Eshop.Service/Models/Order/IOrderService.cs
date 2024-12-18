using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Order
{
    public interface IOrderService : IBaseService<OrderEntity>, IScopedDependency
    {
    }
}
