using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.OrderItem
{
    public class OrderItemRepository : BaseRepository<OrderItemEntity>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
