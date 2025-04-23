using Eshop.Share.ActionFilters;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Receipt
{
    public class SearchReceiptDTO : BaseSearchDTO
    {
        public ReceiptType? Type { get; set; }
        public Guid? AccountPartyId { get; set; }
    }
}