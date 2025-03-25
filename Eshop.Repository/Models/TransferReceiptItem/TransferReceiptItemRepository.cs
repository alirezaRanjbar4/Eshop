using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.TransferReceiptItem
{
    public class TransferReceiptItemRepository : BaseRepository<TransferReceiptItemEntity>, ITransferReceiptItemRepository
    {
        public TransferReceiptItemRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
