using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Supplier;
using Eshop.Service.General;

namespace Eshop.Service.Models.Supplier
{
    public class SupplierService : BaseService<SupplierEntity>, ISupplierService
    {
        public SupplierService(
            IMapper mapper,
            ISupplierRepository SupplierRepository) : base(SupplierRepository, mapper)
        {
        }
    }
}
