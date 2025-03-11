using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.ServiceCategory
{
    public interface IServiceCategoryRepository : IBaseRepository<ServiceCategoryEntity>, IScopedDependency
    {
    }
}
