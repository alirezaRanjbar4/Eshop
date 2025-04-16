using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.ReceiptServiceItem
{
    public interface IReceiptServiceItemRepository : IBaseRepository<ReceiptServiceItemEntity>, IScopedDependency
    {
    }
}
