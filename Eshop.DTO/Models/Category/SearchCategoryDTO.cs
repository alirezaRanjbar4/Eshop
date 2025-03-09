using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models
{
    public class SearchCategoryDTO : BaseSearchDTO
    {
        public Guid StoreId { get; set; }
        public CategoryType? Type { get; set; }
    }
}