using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Order
{
    public interface IOrderRepository : IBaseRepository<OrderEntity>, IScopedDependency
    {
    }
}
