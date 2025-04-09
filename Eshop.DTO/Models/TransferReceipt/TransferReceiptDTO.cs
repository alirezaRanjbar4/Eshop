using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.TransferReceipt
{
    public class TransferReceiptDTO : BaseDTO
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool IsFinalized { get; set; }

        public Guid StoreId { get; set; }
    }
}