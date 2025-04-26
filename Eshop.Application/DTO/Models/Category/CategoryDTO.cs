using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Category
{
    public class CategoryDTO : BaseDTO
    {
        public string Name { get; set; }
        public Guid StoreId { get; set; }
        public Guid? ParentId { get; set; }
        public CategoryType Type { get; set; }
        public string? String_Type { get; set; }
    }
}