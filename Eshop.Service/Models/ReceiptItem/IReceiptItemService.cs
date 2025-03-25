using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ReceiptItem
{
    public interface IReceiptItemService : IBaseService<ReceiptItemEntity>, IScopedDependency
    {
    }
}
