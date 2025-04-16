using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ReceiptServiceItem
{
    public interface IReceiptServiceItemService : IBaseService<ReceiptServiceItemEntity>, IScopedDependency
    {
    }
}
