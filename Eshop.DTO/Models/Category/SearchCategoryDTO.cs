using Eshop.Common.Enum;
using Eshop.DTO.General;

namespace Eshop.DTO.Models.Category
{
    public class SearchCategoryDTO : BaseSearchDTO
    {
        public CategoryType? Type { get; set; }
    }
}