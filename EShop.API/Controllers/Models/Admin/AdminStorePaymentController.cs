using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.DTO.Models.FinancialDocument;
using Eshop.DTO.Models.Store;
using Eshop.Entity.Models;
using Eshop.Service.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Api.Controllers.Models.Admin
{
    [ApiVersion(VersionProperties.V1)]
    [Authorize]
    [DisplayName("AdminStorePayment")]
    public class AdminStorePaymentController : BaseController
    {
        private readonly IBaseService<StorePaymentEntity> _storePaymentService;
        public AdminStorePaymentController(IBaseService<StorePaymentEntity> storePaymentService)
        {
            _storePaymentService = storePaymentService;
        }


        [HttpGet(nameof(GetAllStorePayment))]
        public async Task<List<StorePaymentDTO>> GetAllStorePayment(CancellationToken cancellationToken)
        {
            return await _storePaymentService.GetAllAsync<StorePaymentDTO>(null, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllStorePaymentWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<StorePaymentDTO>>> GetAllStorePaymentWithTotal(SearchStorePaymentDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _storePaymentService.GetAllAsyncWithTotal<StorePaymentDTO>(
                searchDTO,
                x => (searchDTO.StoreId == null || searchDTO.StoreId == Guid.Empty || x.StoreId == searchDTO.StoreId) &&
                     (searchDTO.StartDate == null || x.Date >= searchDTO.StartDate) &&
                     (searchDTO.EndDate == null || x.Date <= searchDTO.EndDate),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpPost(nameof(AddStorePayment)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddStorePayment([FromBody] StorePaymentDTO storePayment, CancellationToken cancellationToken)
        {
            var result = await _storePaymentService.AddAsync(storePayment, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateStorePayment)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateStorePayment([FromBody] StorePaymentDTO storePayment, CancellationToken cancellationToken)
        {
            var result = await _storePaymentService.UpdateAsync(storePayment, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteStorePayment)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteStorePayment(Guid storePaymentId, CancellationToken cancellationToken)
        {
            return await _storePaymentService.DeleteAsync(storePaymentId, true, true, true, cancellationToken);
        }

    }
}
