using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.ReceiptProductItem
{
    public interface IReceiptProductItemRepository : IBaseRepository<ReceiptProductItemEntity>, IScopedDependency
    {
    }
}
