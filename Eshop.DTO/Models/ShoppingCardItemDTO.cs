using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class ShoppingCardItemDTO : BaseDto
    {
        public int Count { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}