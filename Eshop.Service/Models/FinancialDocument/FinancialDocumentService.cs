using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.FinancialDocument;
using Eshop.Service.General;

namespace Eshop.Service.Models.FinancialDocument
{
    public class FinancialDocumentService : BaseService<FinancialDocumentEntity>, IFinancialDocumentService
    {
        public FinancialDocumentService(IMapper mapper, IFinancialDocumentRepository FinancialDocumentRepository) : base(FinancialDocumentRepository, mapper)
        {
        }
    }
}
