using AutoMapper;
using Eshop.DTO.Models.Product;
using Eshop.DTO.Models.TransferReceipt;
using Eshop.Entity.Models;
using Eshop.Repository.Models.TransferReceipt;
using Eshop.Service.General;
using Eshop.Service.Models.ProductWarehouseLocation;
using Eshop.Service.Models.TransferReceiptItem;

namespace Eshop.Service.Models.TransferReceipt
{
    public class TransferReceiptService : BaseService<TransferReceiptEntity>, ITransferReceiptService
    {
        private readonly ITransferReceiptItemService _transferReceiptItemService;
        private readonly IProductWarehouseLocationService _productWarehouseLocationService;
        public TransferReceiptService(
            ITransferReceiptItemService transferReceiptItemService,
            IProductWarehouseLocationService productWarehouseLocationService,
            IMapper mapper,
            ITransferReceiptRepository TransferReceiptRepository) : base(TransferReceiptRepository, mapper)
        {
            _transferReceiptItemService = transferReceiptItemService;
            _productWarehouseLocationService = productWarehouseLocationService;
        }

        public async Task<bool> UpdateTransferReceipt(AddTransferReceiptDTO dto, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<TransferReceiptDTO>(dto);
            var updateResult = await UpdateAsync(model, true, true, cancellationToken);

            var existedItems = await _transferReceiptItemService.GetAllAsync<TransferReceiptItemDTO>(x => x.TransferReceiptId == dto.Id, null, null, false, cancellationToken);

            var addlist = dto.Items.Where(x => x.Id == Guid.Empty).ToList();
            await _transferReceiptItemService.AddRangeAsync(addlist, true, cancellationToken);

            var updatelist = existedItems.Where(x => dto.Items.Select(x => x.Id).Contains(x.Id)).ToList();
            await _transferReceiptItemService.UpdateRangeAsync(updatelist, true, cancellationToken);

            var deleteList = existedItems.Where(x => !dto.Items.Select(x => x.Id).Contains(x.Id)).ToList();
            await _transferReceiptItemService.DeleteRangeAsync(deleteList, true, true, cancellationToken);

            return true;
        }

        public async Task<bool> FinalizeTransferReceipt(Guid receipId, CancellationToken cancellationToken)
        {
            var transferTransferReceipt = await GetAsync<TransferReceiptDTO>(x => x.Id == receipId, null, false, cancellationToken);
            if (transferTransferReceipt.IsFinalized)
                return false;
            transferTransferReceipt.IsFinalized = true;

            var items = await _transferReceiptItemService.GetAllAsync<TransferReceiptItemDTO>(x => x.TransferReceiptId == receipId, null, null, false, cancellationToken);
            foreach (var item in items)
            {
                var enteredWarehouseLocation = await _productWarehouseLocationService.GetAsync<ProductWarehouseLocationDTO>(
                    x => x.ProductId == item.ProductId && x.WarehouseLocationId == item.EnteredWarehouseLocationId,
                    null,
                    false,
                    cancellationToken);
                enteredWarehouseLocation.Count += item.Count;

                var exitedWarehouseLocation = await _productWarehouseLocationService.GetAsync<ProductWarehouseLocationDTO>(
                    x => x.ProductId == item.ProductId && x.WarehouseLocationId == item.ExitedWarehouseLocationId,
                    null,
                    false,
                    cancellationToken);
                exitedWarehouseLocation.Count -= item.Count;

                await _productWarehouseLocationService.UpdateAsync(enteredWarehouseLocation, true, true, cancellationToken);
                await _productWarehouseLocationService.UpdateAsync(exitedWarehouseLocation, true, true, cancellationToken);
            }

            await UpdateAsync(transferTransferReceipt, true, true, cancellationToken);
            return true;
        }
    }
}
