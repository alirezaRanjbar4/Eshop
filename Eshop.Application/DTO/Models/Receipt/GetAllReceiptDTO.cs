using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Receipt
{
    public class GetAllReceiptDTO : BaseDTO
    {
        public int ReceiptNumber { get; set; }
        public string? ReceiptSerial { get; set; }
        public string? Description { get; set; }
        public bool IsFinalized { get; set; }
        public ReceiptType Type { get; set; }
        public string String_Date { get; set; }
        public string String_AccountParty { get; set; }
        public string String_TotalFinalPrice { get; set; }
    }
}