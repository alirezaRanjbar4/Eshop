using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Product
{
    public interface IProductService : IBaseService<ProductEntity>, IScopedDependency
    {
    }
}
