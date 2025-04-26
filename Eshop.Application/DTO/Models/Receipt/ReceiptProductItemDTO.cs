using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Receipt
{
    public class ReceiptProductItemDTO : BaseDTO
    {
        public string? Description { get; set; }
        public float Count { get; set; }
        public long Price { get; set; }
        public long? DiscountPrice { get; set; }
        public int? DiscountPercent { get; set; }
        public int? ValueAdded { get; set; }

        public Guid ReceiptId { get; set; }
        public Guid ProductId { get; set; }
        public Guid WarehouseLocationId { get; set; }
    }
}