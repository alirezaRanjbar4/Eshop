using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.Supplier
{
    public class SupplierRepository : BaseRepository<SupplierEntity>, ISupplierRepository
    {
        public SupplierRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
