using AutoMapper;
using Eshop.Application.DTO.Models.Product;
using Eshop.Application.DTO.Models.Receipt;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.Models.Receipt;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Service.Models.Receipt
{
    public class ReceiptService : BaseService<ReceiptEntity>, IReceiptService
    {
        private readonly IBaseService<ReceiptProductItemEntity> _receiptProductItemService;
        private readonly IBaseService<ReceiptServiceItemEntity> _receiptServiceItemService;
        private readonly IBaseService<ProductWarehouseLocationEntity> _productWarehouseLocationService;
        private readonly IReceiptRepository _receiptRepository;
        public ReceiptService(
            IBaseService<ReceiptProductItemEntity> receiptProductItemService,
            IBaseService<ReceiptServiceItemEntity> receiptServiceItemService,
            IBaseService<ProductWarehouseLocationEntity> productWarehouseLocationService,
            IMapper mapper,
            IReceiptRepository receiptRepository) : base(receiptRepository, mapper)
        {
            _receiptProductItemService = receiptProductItemService;
            _receiptServiceItemService = receiptServiceItemService;
            _productWarehouseLocationService = productWarehouseLocationService;
            _receiptRepository = receiptRepository;
        }

        public async Task<Guid> AddReceipt(AddReceiptDTO dto, CancellationToken cancellationToken)
        {
            var lastReceiptNumber = await _receiptRepository.GetLastReceiptNumber(dto.StoreId, cancellationToken);
            dto.ReceiptNumber = lastReceiptNumber + 1;
            var result = await AddAsync(dto, true, cancellationToken);
            return result.Id;
        }


        public async Task<bool> UpdateReceipt(AddReceiptDTO dto, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<ReceiptDTO>(dto);
            var updateResult = await UpdateAsync(model, true, true, cancellationToken);

            var existedProductItems = await _receiptProductItemService.GetAllAsync<ReceiptProductItemDTO>(x => x.ReceiptId == dto.Id, null, null, false, cancellationToken);
            var existedServiceItems = await _receiptServiceItemService.GetAllAsync<ReceiptServiceItemDTO>(x => x.ReceiptId == dto.Id, null, null, false, cancellationToken);

            #region Product Items
            var addProductItemslist = dto.ProductItems.Where(x => x.Id == Guid.Empty).ToList();
            await _receiptProductItemService.AddRangeAsync(addProductItemslist, true, cancellationToken);

            var updateProductItemslist = dto.ProductItems.Where(x => x.Id != Guid.Empty).ToList();
            await _receiptProductItemService.UpdateRangeAsync(updateProductItemslist, true, cancellationToken);

            var deleteProductItemsList = existedProductItems.Where(x => !dto.ProductItems.Select(x => x.Id).Contains(x.Id)).ToList();
            await _receiptProductItemService.DeleteRangeAsync(deleteProductItemsList, true, true, cancellationToken);
            #endregion

            #region Service Items
            var addServiceItemslist = dto.ServiceItems.Where(x => x.Id == Guid.Empty).ToList();
            await _receiptServiceItemService.AddRangeAsync(addServiceItemslist, true, cancellationToken);

            var updateServiceItemslist = dto.ServiceItems.Where(x => x.Id != Guid.Empty).ToList();
            await _receiptServiceItemService.UpdateRangeAsync(updateServiceItemslist, true, cancellationToken);

            var deleteServiceItemsList = existedServiceItems.Where(x => !dto.ServiceItems.Select(x => x.Id).Contains(x.Id)).ToList();
            await _receiptServiceItemService.DeleteRangeAsync(deleteServiceItemsList, true, true, cancellationToken);
            #endregion

            return true;
        }

        public async Task<bool> FinalizeReceipt(Guid receipId, CancellationToken cancellationToken)
        {
            var receipt = await GetAsync<ReceiptDTO>(x => x.Id == receipId, null, false, cancellationToken);
            if (receipt.IsFinalized)
                return false;
            receipt.IsFinalized = true;

            var productItems = await _receiptProductItemService.GetAllAsync<ReceiptProductItemDTO>(x => x.ReceiptId == receipId, null, null, false, cancellationToken);
            foreach (var item in productItems)
            {
                var productWarehouseLocation = await _productWarehouseLocationService.GetAsync<ProductWarehouseLocationDTO>(
                    x => x.ProductId == item.ProductId && x.WarehouseLocationId == item.WarehouseLocationId,
                    null,
                    false,
                    cancellationToken);

                if (receipt.Type == ReceiptType.Enter)
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
