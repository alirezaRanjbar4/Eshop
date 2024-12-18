using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Vendor;
using Eshop.Service.General;

namespace Eshop.Service.Models.Vendor
{
    public class VendorService : BaseService<VendorEntity>, IVendorService
    {
        public VendorService(IMapper mapper, IVendorRepository VendorRepository) : base(VendorRepository, mapper)
        {
        }
    }
}
