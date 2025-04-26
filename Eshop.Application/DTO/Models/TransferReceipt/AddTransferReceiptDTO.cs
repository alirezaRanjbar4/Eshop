using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.TransferReceipt
{
    public class AddTransferReceiptDTO : BaseDTO
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public Guid StoreId { get; set; }
        public List<TransferReceiptItemDTO> Items { get; set; }
    }
}