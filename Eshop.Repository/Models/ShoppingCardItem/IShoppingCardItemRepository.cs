using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.ShoppingCardItem
{
    public interface IShoppingCardItemRepository : IBaseRepository<ShoppingCardItemEntity>, IScopedDependency
    {
    }
}
