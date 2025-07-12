using AutoMapper;
using Eshop.Application.DTO.Models.FinancialDocument;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;

namespace Eshop.Application.Service.Models.FinancialDocument
{
    public class FinancialDocumentService : BaseService<FinancialDocumentEntity>, IFinancialDocumentService
    {
        private readonly IBaseService<ReceiptFinancialDocumentEntity> _receiptFinancialDocumentService;
        public FinancialDocumentService(
            IBaseService<ReceiptFinancialDocumentEntity> receiptFinancialDocumentService,
            IMapper mapper,
            IBaseRepository<FinancialDocumentEntity> FinancialDocumentRepository) : base(FinancialDocumentRepository, mapper)
        {
            _receiptFinancialDocumentService = receiptFinancialDocumentService;
        }

        public async Task<bool> AddFinancialDocument(FinancialDocumentDTO dTO, CancellationToken cancellationToken)
        {
            var result = await AddAsync(dTO, true, cancellationToken);
            foreach (var item in dTO.ReceiptIds)
            {
                await _receiptFinancialDocumentService.AddAsync(new ReceiptFinancialDocumentDTO()
                {
                    FinancialDocumentId = result.Id,
                    ReceiptId = item
                }, true, cancellationToken);
            }
            return result != null;
        }

        public async Task<bool> UpdateFinancialDocument(FinancialDocumentDTO dTO, CancellationToken cancellationToken)
        {
            var result = await UpdateAsync(dTO, true, true, cancellationToken);
            var existedItems = await _receiptFinancialDocumentService.GetAllAsync<ReceiptFinancialDocumentDTO>(x => x.FinancialDocumentId == dTO.Id, null, null, false, cancellationToken);

            var addList = dTO.ReceiptIds.Where(x => !existedItems.Select(z => z.ReceiptId).Contains(x)).Select(x => new ReceiptFinancialDocumentDTO()
            {
                FinancialDocumentId = result.Id,
                ReceiptId = x
            }).ToList();
            await _receiptFinancialDocumentService.AddRangeAsync(addList, true, cancellationToken);

            var deleteList = existedItems.Where(x => !dTO.ReceiptIds.Contains(x.ReceiptId)).ToList();
            await _receiptFinancialDocumentService.DeleteRangeAsync(deleteList, true, true, cancellationToken);

            return result != null;
        }
    }
}
