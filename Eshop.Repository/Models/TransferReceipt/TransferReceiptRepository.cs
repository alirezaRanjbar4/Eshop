using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.TransferReceipt
{
    public class TransferReceiptRepository : BaseRepository<TransferReceiptEntity>, ITransferReceiptRepository
    {
        public TransferReceiptRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
