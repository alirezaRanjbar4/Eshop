using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ServiceCategory
{
    public interface IServiceCategoryService : IBaseService<ServiceCategoryEntity>, IScopedDependency
    {
    }
}
