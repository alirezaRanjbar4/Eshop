using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Infrastructure.Repository.Models.Receipt
{
    public interface IReceiptRepository : IBaseRepository<ReceiptEntity>, IScopedDependency
    {
        Task<int> GetLastReceiptNumber(Guid storeId, CancellationToken cancellationToken);
    }
}
