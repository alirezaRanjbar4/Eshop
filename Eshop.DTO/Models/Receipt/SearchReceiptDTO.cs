using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models.Receipt
{
    public class SearchReceiptDTO : BaseSearchDTO
    {
        public Guid StoreId { get; set; }
        public ReceiptType? Type { get; set; }
    }
}