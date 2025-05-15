using Eshop.Application.DTO.Models.Receipt;
using Eshop.Application.Service.Models.Receipt;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Models
{
    [Authorize]
    [DisplayName("Receipt")]
    public class ReceiptController : BaseController
    {
        private readonly IReceiptService _receiptService;
        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }


        [HttpPost(nameof(GetAllReceiptWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<ReceiptDTO>>> GetAllReceiptWithTotal([FromBody] SearchReceiptDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _receiptService.GetAllAsyncWithTotal<ReceiptDTO>(
                searchDTO,
                x => x.StoreId == CurrentUserStoreId &&
                (searchDTO.Type == null || x.Type == searchDTO.Type) &&
                (searchDTO.AccountPartyId == null || searchDTO.AccountPartyId == Guid.Empty || x.AccountPartyId == searchDTO.AccountPartyId),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(GetAllReceipt)), /*DisplayName(nameof(PermissionResourceEnums.GetAllPermission))*/]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<List<SimpleReceiptDTO>> GetAllReceipt([FromBody] SimpleSearchReceiptDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _receiptService.GetAllAsync<SimpleReceiptDTO>(
                x => x.StoreId == CurrentUserStoreId &&
                (searchDTO.Type == null || x.Type == searchDTO.Type) &&
                (string.IsNullOrEmpty(searchDTO.SearchTerm) || x.AccountParty.Name.Contains(searchDTO.SearchTerm) || x.ReceiptSerial.Contains(searchDTO.SearchTerm)),
                i => i.Include(x => x.AccountParty),
                o => o.OrderByDescending(x => x.CreateDate),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(AddReceipt)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddReceipt([FromBody] AddReceiptDTO receipt, CancellationToken cancellationToken)
        {
            return await _receiptService.AddReceipt(receipt, cancellationToken);
        }


        [HttpPost(nameof(UpdateReceipt)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateReceipt([FromBody] AddReceiptDTO receipt, CancellationToken cancellationToken)
        {
            return await _receiptService.UpdateReceipt(receipt, cancellationToken);
        }


        [HttpDelete(nameof(DeleteReceipt)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteReceipt(Guid receiptId, CancellationToken cancellationToken)
        {
            return await _receiptService.DeleteAsync(receiptId, true, true, true, cancellationToken);
        }


        [HttpPost(nameof(FinalizeReceipt))/*, DisplayName(nameof(PermissionResourceEnums.FinalizeReceipt))*/]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> FinalizeReceipt([FromBody] Guid receiptId, CancellationToken cancellationToken)
        {
            return await _receiptService.FinalizeReceipt(receiptId, cancellationToken);
        }

    }
}
