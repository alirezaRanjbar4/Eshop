using Eshop.DTO.General;

namespace Eshop.DTO.Models.Product
{
    public class ProductCategoryDTO : BaseDTO
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
    }
}