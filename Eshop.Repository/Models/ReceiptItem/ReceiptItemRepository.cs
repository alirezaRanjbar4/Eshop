using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ReceiptItem
{
    public class ReceiptItemRepository : BaseRepository<ReceiptItemEntity>, IReceiptItemRepository
    {
        public ReceiptItemRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
