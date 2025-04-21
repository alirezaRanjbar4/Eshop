using Eshop.Common.Enum;
using Eshop.DTO.General;

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