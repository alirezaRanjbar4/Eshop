using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.Category
{
    public class SearchCategoryDTO : BaseSearchDTO
    {
        public CategoryType? Type { get; set; }
    }
}