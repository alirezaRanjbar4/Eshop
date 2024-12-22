using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class CategoryDTO : BaseDto
    {
        public string Name { get; set; }
        public Guid StoreId { get; set; }
        public Guid ParentId { get; set; } 

        public List<CategoryDTO> Childs { get; set; }
        public List<ProductCategoryDTO> ProductCategories { get; set; }
    }
}