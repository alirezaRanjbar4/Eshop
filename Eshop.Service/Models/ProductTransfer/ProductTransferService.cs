using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ProductTransfer;
using Eshop.Service.General;

namespace Eshop.Service.Models.ProductTransfer
{
    public class ProductTransferService : BaseService<ProductTransferEntity>, IProductTransferService
    {
        public ProductTransferService(IMapper mapper, IProductTransferRepository ProductTransferRepository) : base(ProductTransferRepository, mapper)
        {
        }
    }
}
