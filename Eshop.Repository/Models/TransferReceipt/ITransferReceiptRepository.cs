using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.TransferReceipt
{
    public interface ITransferReceiptRepository : IBaseRepository<TransferReceiptEntity>, IScopedDependency
    {
    }
}
