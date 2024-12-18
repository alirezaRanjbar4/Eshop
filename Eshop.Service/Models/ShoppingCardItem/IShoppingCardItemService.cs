using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ShoppingCardItem
{
    public interface IShoppingCardItemService : IBaseService<ShoppingCardItemEntity>, IScopedDependency
    {
    }
}
