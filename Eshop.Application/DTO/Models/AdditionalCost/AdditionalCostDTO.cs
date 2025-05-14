using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.AdditionalCost
{
    public class AdditionalCostDTO : BaseDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }

        public Guid StoreId { get; set; }

        public string? String_Date { get; set; }
    }
}