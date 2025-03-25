using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ReceiptFinancialDocument
{
    public interface IReceiptFinancialDocumentService : IBaseService<ReceiptFinancialDocumentEntity>, IScopedDependency
    {
    }
}
