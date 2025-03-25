using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.Receipt
{
    public class ReceiptRepository : BaseRepository<ReceiptEntity>, IReceiptRepository
    {
        public ReceiptRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
