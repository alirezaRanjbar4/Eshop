using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models.FinancialDocument;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.FinancialDocument
{
    public interface IFinancialDocumentService : IBaseService<FinancialDocumentEntity>, IScopedDependency
    {
        Task<bool> AddFinancialDocument(AddFinancialDocumentDTO dTO, CancellationToken cancellationToken);
        Task<bool> UpdateFinancialDocument(AddFinancialDocumentDTO dTO, CancellationToken cancellationToken);
    }
}
