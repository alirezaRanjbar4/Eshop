using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.DTO.General;
using Eshop.DTO.Models.TransferReceipt;
using Eshop.Service.Models.TransferReceipt;
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
    [DisplayName("TransferReceipt")]
    public class TransferReceiptController : BaseController
    {
        private readonly ITransferReceiptService _transferReceiptService;
        public TransferReceiptController(ITransferReceiptService transferReceiptService)
        {
            _transferReceiptService = transferReceiptService;
        }


        [HttpPost(nameof(GetAllTransferReceiptWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<TransferReceiptDTO>>> GetAllTransferReceiptWithTotal(BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _transferReceiptService.GetAllAsyncWithTotal<TransferReceiptDTO>(
                searchDTO,
                x => x.StoreId == CurrentUserStoreId,
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpPost(nameof(AddTransferReceipt)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddTransferReceipt([FromBody] AddTransferReceiptDTO transferReceipt, CancellationToken cancellationToken)
        {
            var result = await _transferReceiptService.AddAsync(transferReceipt, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateTransferReceipt)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateTransferReceipt([FromBody] AddTransferReceiptDTO transferReceipt, CancellationToken cancellationToken)
        {
            return await _transferReceiptService.UpdateTransferReceipt(transferReceipt, cancellationToken);
        }


        [HttpDelete(nameof(DeleteTransferReceipt)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteTransferReceipt(Guid transferReceiptId, CancellationToken cancellationToken)
        {
            return await _transferReceiptService.DeleteAsync(transferReceiptId, true, true, true, cancellationToken);
        }


        [HttpPost(nameof(FinalizeTransferReceipt))/*, DisplayName(nameof(PermissionResourceEnums.FinalizeTransferReceipt))*/]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> FinalizeTransferReceipt([FromBody] Guid transferReceiptId, CancellationToken cancellationToken)
        {
            return await _transferReceiptService.FinalizeTransferReceipt(transferReceiptId, cancellationToken);
        }

    }
}
