using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Product
{
    public interface IProductRepository : IBaseRepository<ProductEntity>, IScopedDependency
    {
    }
}
