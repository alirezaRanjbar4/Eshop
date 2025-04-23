using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.FinancialDocument
{
    public class ReceiptFinancialDocumentDTO : BaseDTO
    {
        public Guid ReceiptId { get; set; }
        public Guid FinancialDocumentId { get; set; }
    }
}