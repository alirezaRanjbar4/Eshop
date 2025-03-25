using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ReceiptItem;
using Eshop.Service.General;

namespace Eshop.Service.Models.ReceiptItem
{
    public class ReceiptItemService : BaseService<ReceiptItemEntity>, IReceiptItemService
    {
        public ReceiptItemService(IMapper mapper, IReceiptItemRepository ReceiptItemRepository) : base(ReceiptItemRepository, mapper)
        {
        }
    }
}
