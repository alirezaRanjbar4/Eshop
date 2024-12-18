using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.OrderItem;
using Eshop.Service.General;

namespace Eshop.Service.Models.OrderItem
{
    public class OrderItemService : BaseService<OrderItemEntity>, IOrderItemService
    {
        public OrderItemService(IMapper mapper, IOrderItemRepository OrderItemRepository) : base(OrderItemRepository, mapper)
        {
        }
    }
}
