using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models.Receipt;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Receipt
{
    public interface IReceiptService : IBaseService<ReceiptEntity>, IScopedDependency
    {
        Task<bool> UpdateReceipt(AddReceiptDTO dto, CancellationToken cancellationToken);
        Task<bool> FinalizeReceipt(Guid receiptId, CancellationToken cancellationToken);
    }
}
