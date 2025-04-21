using Eshop.Common.Enum;
using Eshop.DTO.General;

namespace Eshop.DTO.Models.Receipt
{
    public class SearchReceiptDTO : BaseSearchDTO
    {
        public ReceiptType? Type { get; set; }
        public Guid? AccountPartyId { get; set; }
    }
}