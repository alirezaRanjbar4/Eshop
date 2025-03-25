using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ReceiptFinancialDocument;
using Eshop.Service.General;

namespace Eshop.Service.Models.ReceiptFinancialDocument
{
    public class ReceiptFinancialDocumentService : BaseService<ReceiptFinancialDocumentEntity>, IReceiptFinancialDocumentService
    {
        public ReceiptFinancialDocumentService(IMapper mapper, IReceiptFinancialDocumentRepository ReceiptFinancialDocumentRepository) : base(ReceiptFinancialDocumentRepository, mapper)
        {
        }
    }
}
