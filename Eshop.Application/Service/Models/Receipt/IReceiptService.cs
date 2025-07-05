using Eshop.Application.DTO.Models.Receipt;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Application.Service.Models.Receipt
{
    public interface IReceiptService : IBaseService<ReceiptEntity>, IScopedDependency
    {
        Task<Guid> AddReceipt(AddReceiptDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateReceipt(AddReceiptDTO dto, CancellationToken cancellationToken);
        Task<bool> FinalizeReceipt(Guid receiptId, CancellationToken cancellationToken);
    }
}
