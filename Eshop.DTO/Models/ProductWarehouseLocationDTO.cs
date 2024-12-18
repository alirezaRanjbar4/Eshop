using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class ProductWarehouseLocationDTO : BaseDto
    {
        public int Count { get; set; }
        public Guid WarehouseLocationId { get; set; }
        public Guid ProductId { get; set; }
    }
}