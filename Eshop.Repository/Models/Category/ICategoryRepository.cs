using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Category
{
    public interface ICategoryRepository : IBaseRepository<CategoryEntity>, IScopedDependency
    {
    }
}
