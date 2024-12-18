using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class OrderItemDTO : BaseDto
    {
        public int RequestedAmount { get; set; }
        public int FinalAmount { get; set; }
        public long PrimaryPrice { get; set; }
        public long FinalPrice { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}