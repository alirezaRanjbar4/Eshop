using Eshop.Common.Enum;
using Eshop.DTO.General;

namespace Eshop.DTO.Models.Product
{
    public class ProductTransferDTO : BaseDTO
    {
        public int Count { get; set; }
        public string? Description { get; set; }
        public ProductTransferType Type { get; set; }

        public Guid ProductId { get; set; }
        public Guid WarehouseLocationId { get; set; }
    }
}