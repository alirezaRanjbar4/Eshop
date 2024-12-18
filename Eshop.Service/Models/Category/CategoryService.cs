using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Category;
using Eshop.Service.General;

namespace Eshop.Service.Models.Category
{
    public class CategoryService : BaseService<CategoryEntity>, ICategoryService
    {
        public CategoryService(IMapper mapper, ICategoryRepository CategoryRepository) : base(CategoryRepository, mapper)
        {
        }
    }
}
