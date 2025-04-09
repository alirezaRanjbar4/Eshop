using Eshop.DTO.General;
using Eshop.Common.Enum;

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