using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ReceiptProductItem
{
    public interface IReceiptProductItemService : IBaseService<ReceiptProductItemEntity>, IScopedDependency
    {
    }
}
