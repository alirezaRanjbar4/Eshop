using Eshop.Application.DTO.Models.TransferReceipt;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Application.Service.Models.TransferReceipt
{
    public interface ITransferReceiptService : IBaseService<TransferReceiptEntity>, IScopedDependency
    {
        Task<bool> UpdateTransferReceipt(AddTransferReceiptDTO dto, CancellationToken cancellationToken);
        Task<bool> FinalizeTransferReceipt(Guid transferTransferReceiptId, CancellationToken cancellationToken);
    }
}
