using Eshop.Share.ActionFilters;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Category
{
    public class SearchCategoryDTO : BaseSearchDTO
    {
        public CategoryType? Type { get; set; }
    }
}