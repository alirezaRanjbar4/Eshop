using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.ReceiptFinancialDocument
{
    public interface IReceiptFinancialDocumentRepository : IBaseRepository<ReceiptFinancialDocumentEntity>, IScopedDependency
    {
    }
}
