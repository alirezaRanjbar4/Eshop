using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Receipt
{
    public class SimpleReceiptDTO : BaseDTO
    {
        public int ReceiptNumber { get; set; }
        public string? ReceiptSerial { get; set; }
        public string AccountParty { get; set; }
    }
}