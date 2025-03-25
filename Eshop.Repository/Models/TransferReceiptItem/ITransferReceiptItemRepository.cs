using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.TransferReceiptItem
{
    public interface ITransferReceiptItemRepository : IBaseRepository<TransferReceiptItemEntity>, IScopedDependency
    {
    }
}
