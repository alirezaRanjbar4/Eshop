using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.Vendor
{
    public class VendorRepository : BaseRepository<VendorEntity>, IVendorRepository
    {
        public VendorRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
