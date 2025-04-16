using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ReceiptProductItem
{
    public class ReceiptProductItemRepository : BaseRepository<ReceiptProductItemEntity>, IReceiptProductItemRepository
    {
        public ReceiptProductItemRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
