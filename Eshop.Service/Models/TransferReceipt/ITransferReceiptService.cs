using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models.TransferReceipt;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.TransferReceipt
{
    public interface ITransferReceiptService : IBaseService<TransferReceiptEntity>, IScopedDependency
    {
        Task<bool> UpdateTransferReceipt(AddTransferReceiptDTO dto, CancellationToken cancellationToken);
        Task<bool> FinalizeTransferReceipt(Guid transferTransferReceiptId, CancellationToken cancellationToken);
    }
}
