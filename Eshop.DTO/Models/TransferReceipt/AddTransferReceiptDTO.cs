using Eshop.DTO.General;

namespace Eshop.DTO.Models.TransferReceipt
{
    public class AddTransferReceiptDTO : BaseDto
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public Guid StoreId { get; set; }
        public List<TransferReceiptItemDTO> Items { get; set; }
    }
}