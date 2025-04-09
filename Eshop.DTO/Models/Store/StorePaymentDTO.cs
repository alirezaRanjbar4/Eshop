using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.Store
{
    public class StorePaymentDTO : BaseDTO
    {
        public string? Description { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public FinancialDocumentPaymentMethod PaymentMethod { get; set; }

        public Guid StoreId { get; set; }
    }
}