using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ProductCategory;
using Eshop.Service.General;

namespace Eshop.Service.Models.ProductCategory
{
    public class ProductCategoryService : BaseService<ProductCategoryEntity>, IProductCategoryService
    {
        public ProductCategoryService(IMapper mapper, IProductCategoryRepository ProductCategoryRepository) : base(ProductCategoryRepository, mapper)
        {
        }
    }
}
