using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ReceiptFinancialDocument
{
    public class ReceiptFinancialDocumentRepository : BaseRepository<ReceiptFinancialDocumentEntity>, IReceiptFinancialDocumentRepository
    {
        public ReceiptFinancialDocumentRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
