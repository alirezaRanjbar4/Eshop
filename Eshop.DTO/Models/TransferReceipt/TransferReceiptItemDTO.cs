using Eshop.DTO.General;

namespace Eshop.DTO.Models.TransferReceipt
{
    public class TransferReceiptItemDTO : BaseDTO
    {
        public string? Description { get; set; }
        public float Count { get; set; }

        public Guid TransferReceiptId { get; set; }
        public Guid ProductId { get; set; }
        public Guid EnteredWarehouseLocationId { get; set; }
        public Guid ExitedWarehouseLocationId { get; set; }
    }
}