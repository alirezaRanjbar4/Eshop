using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ReceiptProductItem;
using Eshop.Service.General;

namespace Eshop.Service.Models.ReceiptProductItem
{
    public class ReceiptProductItemService : BaseService<ReceiptProductItemEntity>, IReceiptProductItemService
    {
        public ReceiptProductItemService(IMapper mapper, IReceiptProductItemRepository ReceiptProductItemRepository) : base(ReceiptProductItemRepository, mapper)
        {
        }
    }
}
