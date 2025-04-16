using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ReceiptServiceItem;
using Eshop.Service.General;

namespace Eshop.Service.Models.ReceiptServiceItem
{
    public class ReceiptServiceItemService : BaseService<ReceiptServiceItemEntity>, IReceiptServiceItemService
    {
        public ReceiptServiceItemService(IMapper mapper, IReceiptServiceItemRepository ReceiptServiceItemRepository) : base(ReceiptServiceItemRepository, mapper)
        {
        }
    }
}
