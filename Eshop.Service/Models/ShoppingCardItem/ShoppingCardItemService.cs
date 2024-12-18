using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ShoppingCardItem;
using Eshop.Service.General;

namespace Eshop.Service.Models.ShoppingCardItem
{
    public class ShoppingCardItemService : BaseService<ShoppingCardItemEntity>, IShoppingCardItemService
    {
        public ShoppingCardItemService(IMapper mapper, IShoppingCardItemRepository ShoppingCardItemRepository) : base(ShoppingCardItemRepository, mapper)
        {
        }
    }
}
