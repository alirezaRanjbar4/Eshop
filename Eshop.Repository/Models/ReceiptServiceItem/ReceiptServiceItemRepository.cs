using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ReceiptServiceItem
{
    public class ReceiptServiceItemRepository : BaseRepository<ReceiptServiceItemEntity>, IReceiptServiceItemRepository
    {
        public ReceiptServiceItemRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
