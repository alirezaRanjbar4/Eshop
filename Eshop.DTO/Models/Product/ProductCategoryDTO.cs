using Eshop.DTO.General;

namespace Eshop.DTO.Models.Product
{
    public class ProductCategoryDTO : BaseDto
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
    }
}