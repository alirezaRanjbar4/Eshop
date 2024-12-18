using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Order;
using Eshop.Service.General;

namespace Eshop.Service.Models.Order
{
    public class OrderService : BaseService<OrderEntity>, IOrderService
    {
        public OrderService(IMapper mapper, IOrderRepository OrderRepository) : base(OrderRepository, mapper)
        {
        }
    }
}
