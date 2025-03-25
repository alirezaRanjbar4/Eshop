using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Receipt
{
    public interface IReceiptRepository : IBaseRepository<ReceiptEntity>, IScopedDependency
    {
    }
}
