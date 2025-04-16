using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.Store
{
    public class SearchStorePaymentDTO : BaseSearchDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? StoreId { get; set; }
    }
}