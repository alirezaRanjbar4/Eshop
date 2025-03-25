using AutoMapper;
using Eshop.DTO.Models.Product;
using Eshop.DTO.Models.Receipt;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Receipt;
using Eshop.Service.General;
using Eshop.Service.Models.ProductWarehouseLocation;
using Eshop.Service.Models.ReceiptItem;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Service.Models.Receipt
{
    public class ReceiptService : BaseService<ReceiptEntity>, IReceiptService
    {
        private readonly IReceiptItemService _receiptItemService;
        private readonly IProductWarehouseLocationService _productWarehouseLocationService;
        public ReceiptService(
            IReceiptItemService receiptItemService,
            IProductWarehouseLocationService productWarehouseLocationService,
            IMapper mapper,
            IReceiptRepository ReceiptRepository) : base(ReceiptRepository, mapper)
        {
            _receiptItemService = receiptItemService;
            _productWarehouseLocationService = productWarehouseLocationService;
        }

        public async Task<bool> UpdateReceipt(AddReceiptDTO dto, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<ReceiptDTO>(dto);
            var updateResult = await UpdateAsync(model, true, true, cancellationToken);

            var existedItems = await _receiptItemService.GetAllAsync<ReceiptItemDTO>(x => x.ReceiptId == dto.Id, null, null, false, cancellationToken);

            var addlist = dto.Items.Where(x => x.Id == Guid.Empty).ToList();
            await _receiptItemService.AddRangeAsync(addlist, true, cancellationToken);

            var updatelist = existedItems.Where(x => dto.Items.Select(x => x.Id).Contains(x.Id)).ToList();
            await _receiptItemService.UpdateRangeAsync(updatelist, true, cancellationToken);

            var deleteList = existedItems.Where(x => !dto.Items.Select(x => x.Id).Contains(x.Id)).ToList();
            await _receiptItemService.DeleteRangeAsync(deleteList, true, true, cancellationToken);

            return true;
        }

        public async Task<bool> FinalizeReceipt(Guid receipId, CancellationToken cancellationToken)
        {
            var receipt = await GetAsync<ReceiptDTO>(x => x.Id == receipId, null, false, cancellationToken);
            if (receipt.IsFinalized)
                return false;
            receipt.IsFinalized = true;

            var items = await _receiptItemService.GetAllAsync<ReceiptItemDTO>(x => x.ReceiptId == receipId, null, null, false, cancellationToken);
            foreach (var item in items)
            {
                var productWarehouseLocation = await _productWarehouseLocationService.GetAsync<ProductWarehouseLocationDTO>(
                    x => x.ProductId == item.ProductId && x.WarehouseLocationId == item.WarehouseLocationId,
                    null,
                    false,
                    cancellationToken);

                if (receipt.Type == Enum.ReceiptType.Enter)
                    productWarehouseLocation.Count += item.Count;
                else
                    productWarehouseLocation.Count -= item.Count;

                await _productWarehouseLocationService.UpdateAsync(productWarehouseLocation, true, true, cancellationToken);
            }

            await UpdateAsync(receipt, true, true, cancellationToken);
            return true;
        }
    }
}
