using Eshop.DTO.General;

namespace Eshop.DTO.Models.FinancialDocument
{
    public class ReceiptFinancialDocumentDTO : BaseDto
    {
        public Guid ReceiptId { get; set; }
        public Guid FinancialDocumentId { get; set; }
    }
}