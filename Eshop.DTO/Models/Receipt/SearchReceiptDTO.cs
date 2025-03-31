using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.Receipt
{
    public class SearchReceiptDTO : BaseSearchDTO
    {
        public ReceiptType? Type { get; set; }
    }
}