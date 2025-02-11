using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ProductTransfer
{
    public class ProductTransferRepository : BaseRepository<ProductTransferEntity>, IProductTransferRepository
    {
        public ProductTransferRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
