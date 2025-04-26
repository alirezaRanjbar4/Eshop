using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Product
{
    public class ProductCategoryDTO : BaseDTO
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
    }
}