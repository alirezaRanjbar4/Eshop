using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Receipt
{
    public class SimpleSearchReceiptDTO 
    {
        public ReceiptType? Type { get; set; }
        public string? SearchTerm { get; set; }
    }
}