using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.FinancialDocument
{
    public interface IFinancialDocumentRepository : IBaseRepository<FinancialDocumentEntity>, IScopedDependency
    {
    }
}
