using Eshop.Domain.Models;
using Eshop.Infrastructure.DBContext;
using Eshop.Infrastructure.Repository.General;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Infrastructure.Repository.Models.Receipt
{
    public class ReceiptRepository : BaseRepository<ReceiptEntity>, IReceiptRepository
    {
        public ReceiptRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }

        public async Task<int> GetLastReceiptNumber(Guid storeId, CancellationToken cancellationToken)
        {
            return await dbSet
                .Where(x => x.StoreId == storeId)
                .Select(x => x.ReceiptNumber)
                .OrderByDescending(x => x)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
