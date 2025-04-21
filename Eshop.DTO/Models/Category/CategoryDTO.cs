using Eshop.Common.Enum;
using Eshop.DTO.General;

namespace Eshop.DTO.Models.Category
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