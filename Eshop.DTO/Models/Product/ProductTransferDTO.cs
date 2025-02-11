using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models.Product
{
    public class ProductTransferDTO : BaseDto
    {
        public int Count { get; set; }
        public string? Description { get; set; }
        public ProductTransferType Type { get; set; }

        public Guid ProductId { get; set; }
        public Guid WarehouseLocationId { get; set; }
    }
}