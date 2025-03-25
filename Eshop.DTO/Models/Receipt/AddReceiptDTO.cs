using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models.Receipt
{
    public class AddReceiptDTO : BaseDto
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public ReceiptType Type { get; set; }
        public Guid AccountPartyId { get; set; }
        public Guid StoreId { get; set; }
        public List<ReceiptItemDTO> Items { get; set; }
    }
}