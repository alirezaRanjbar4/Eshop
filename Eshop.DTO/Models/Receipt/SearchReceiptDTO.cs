using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models.Category
{
    public class SearchReceiptDTO : BaseSearchDTO
    {
        public Guid StoreId { get; set; }
        public ReceiptType? Type { get; set; }
    }
}