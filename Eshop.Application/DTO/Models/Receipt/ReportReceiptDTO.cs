using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Receipt
{
    public class ReportReceiptDTO : BaseDTO
    {
        public DateTime Date { get; set; }
        public ReceiptType Type { get; set; }
        public long TotalFinalPrice { get; set; }
    }
}