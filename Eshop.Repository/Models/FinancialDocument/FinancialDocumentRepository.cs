using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.FinancialDocument
{
    public class FinancialDocumentRepository : BaseRepository<FinancialDocumentEntity>, IFinancialDocumentRepository
    {
        public FinancialDocumentRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
