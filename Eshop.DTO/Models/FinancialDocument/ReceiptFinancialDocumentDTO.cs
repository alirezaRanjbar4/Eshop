using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models.FinancialDocument
{
    public class ReceiptFinancialDocumentDTO : BaseDto
    {
        public Guid ReceiptId { get; set; }
        public Guid FinancialDocumentId { get; set; }
    }
}