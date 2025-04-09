using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.Receipt
{
    public class ReceiptDTO : BaseDTO
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool IsFinalized { get; set; }
        public ReceiptType Type { get; set; }

        public Guid AccountPartyId { get; set; }
        public Guid StoreId { get; set; }
    }
}