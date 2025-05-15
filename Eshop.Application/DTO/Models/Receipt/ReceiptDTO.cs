using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Receipt
{
    public class ReceiptDTO : BaseDTO
    {
        public int ReceiptNumber { get; set; }
        public string? ReceiptSerial { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool IsFinalized { get; set; }
        public ReceiptType Type { get; set; }

        public Guid AccountPartyId { get; set; }
        public Guid StoreId { get; set; }
    }
}