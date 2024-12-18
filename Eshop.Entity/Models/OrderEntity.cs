using Eshop.Entity.General;
using Eshop.Enum;

namespace Eshop.Entity.Models
{
    public class OrderEntity : BaseTrackedModel, IBaseEntity
    {
        public OrderStatus Status { get; set; }

        public Guid CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
    }
}