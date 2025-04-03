using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.DTO.Models.Receipt;
using Eshop.Service.Models.Receipt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Api.Controllers.Models
{
    [ApiVersion(VersionProperties.V1)]
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
                false, cancellationToken);
        }


        [HttpPost(nameof(AddReceipt)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddReceipt([FromBody] AddReceiptDTO receipt, CancellationToken cancellationToken)
        {
            var result = await _receiptService.AddAsync(receipt, true, cancellationToken);
            return result != null;
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
