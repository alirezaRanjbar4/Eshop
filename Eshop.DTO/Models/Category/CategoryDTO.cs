using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models.Category
{
    public class CategoryDTO : BaseDto
    {
        public string Name { get; set; }
        public Guid StoreId { get; set; }
        public Guid? ParentId { get; set; }
        public CategoryType Type { get; set; }
        public string? String_Type { get; set; }
    }
}