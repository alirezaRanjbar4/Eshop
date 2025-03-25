using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.ReceiptItem
{
    public interface IReceiptItemRepository : IBaseRepository<ReceiptItemEntity>, IScopedDependency
    {
    }
}
