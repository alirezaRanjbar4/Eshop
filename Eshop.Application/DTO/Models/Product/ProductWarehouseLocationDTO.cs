using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Product
{
    public class ProductWarehouseLocationDTO : BaseDTO
    {
        public float Count { get; set; }
        public Guid WarehouseLocationId { get; set; }
        public Guid ProductId { get; set; }
    }
}