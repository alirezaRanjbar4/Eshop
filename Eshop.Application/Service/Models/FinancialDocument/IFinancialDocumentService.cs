using Eshop.Application.DTO.Models.FinancialDocument;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Application.Service.Models.FinancialDocument
{
    public interface IFinancialDocumentService : IBaseService<FinancialDocumentEntity>, IScopedDependency
    {
        Task<bool> AddFinancialDocument(FinancialDocumentDTO dTO, CancellationToken cancellationToken);
        Task<bool> UpdateFinancialDocument(FinancialDocumentDTO dTO, CancellationToken cancellationToken);
    }
}
