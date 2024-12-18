using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models
{
    public class OrderDTO : BaseDto
    {
        public OrderStatus Status { get; set; }
        public Guid CustomerId { get; set; }
        public Guid StoreId { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }
}