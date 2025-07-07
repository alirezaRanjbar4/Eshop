using AutoMapper;
using Eshop.Application.DTO.Models.Product;
using Eshop.Application.DTO.Models.TransferReceipt;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;

namespace Eshop.Application.Service.Models.TransferReceipt
{
    public class TransferReceiptService : BaseService<TransferReceiptEntity>, ITransferReceiptService
    {
        private readonly IBaseService<TransferReceiptItemEntity> _transferReceiptItemService;
        private readonly IBaseService<ProductWarehouseLocationEntity> _productWarehouseLocationService;
        public TransferReceiptService(
            IBaseService<TransferReceiptItemEntity> transferReceiptItemService,
            IBaseService<ProductWarehouseLocationEntity> productWarehouseLocationService,
            IMapper mapper,
            IBaseRepository<TransferReceiptEntity> TransferReceiptRepository) : base(TransferReceiptRepository, mapper)
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
                if (enteredWarehouseLocation != null)
                {
                    enteredWarehouseLocation.Count += item.Count;
                    await _productWarehouseLocationService.UpdateAsync(enteredWarehouseLocation, true, true, cancellationToken);
                }
                else
                {
                    enteredWarehouseLocation = new ProductWarehouseLocationDTO
                    {
                        ProductId = item.ProductId,
                        WarehouseLocationId = item.EnteredWarehouseLocationId,
                        Count = item.Count
                    };
                    await _productWarehouseLocationService.AddAsync(enteredWarehouseLocation, true, cancellationToken);
                }  

                var exitedWarehouseLocation = await _productWarehouseLocationService.GetAsync<ProductWarehouseLocationDTO>(
                    x => x.ProductId == item.ProductId && x.WarehouseLocationId == item.ExitedWarehouseLocationId,
                    null,
                    false,
                    cancellationToken);
                
                if (exitedWarehouseLocation!=null)
                {
                    exitedWarehouseLocation.Count -= item.Count;
                    await _productWarehouseLocationService.UpdateAsync(exitedWarehouseLocation, true, true, cancellationToken);
                }
                else
                {
                    exitedWarehouseLocation = new ProductWarehouseLocationDTO
                    {
                        ProductId = item.ProductId,
                        WarehouseLocationId = item.ExitedWarehouseLocationId,
                        Count = -item.Count
                    };
                    await _productWarehouseLocationService.AddAsync(exitedWarehouseLocation, true, cancellationToken);
                }
            }

            await UpdateAsync(transferTransferReceipt, true, true, cancellationToken);
            return true;
        }
    }
}
