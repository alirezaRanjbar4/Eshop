using Eshop.Share.ActionFilters;

namespace Eshop.Application.DTO.Models.Store
{
    public class SearchStorePaymentDTO : BaseSearchDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? StoreId { get; set; }
    }
}