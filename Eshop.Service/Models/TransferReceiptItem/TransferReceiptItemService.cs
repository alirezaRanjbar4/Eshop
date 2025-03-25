using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.TransferReceiptItem;
using Eshop.Service.General;

namespace Eshop.Service.Models.TransferReceiptItem
{
    public class TransferReceiptItemService : BaseService<TransferReceiptItemEntity>, ITransferReceiptItemService
    {
        public TransferReceiptItemService(IMapper mapper, ITransferReceiptItemRepository TransferReceiptItemRepository) : base(TransferReceiptItemRepository, mapper)
        {
        }
    }
}
