using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ShoppingCardItem
{
    public class ShoppingCardItemRepository : BaseRepository<ShoppingCardItemEntity>, IShoppingCardItemRepository
    {
        public ShoppingCardItemRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
