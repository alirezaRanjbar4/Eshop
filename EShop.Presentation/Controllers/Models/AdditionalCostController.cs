using Eshop.Application.DTO.Models.AdditionalCost;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Models
{
    [Authorize]
    [DisplayName("AdditionalCost")]
    public class AdditionalCostController : BaseController
    {
        private readonly IBaseService<AdditionalCostEntity> _additionalCostService;
        public AdditionalCostController(IBaseService<AdditionalCostEntity> additionalCostService)
        {
            _additionalCostService = additionalCostService;
        }


        [HttpPost(nameof(GetAllAdditionalCostWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<AdditionalCostDTO>>> GetAllAdditionalCostWithTotal([FromBody] BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _additionalCostService.GetAllAsyncWithTotal<AdditionalCostDTO>(
                searchDTO,
                x => x.StoreId == CurrentUserStoreId &&
                (string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Title.Contains(searchDTO.SearchTerm)),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(AddAdditionalCost)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddAdditionalCost([FromBody] AdditionalCostDTO additionalCost, CancellationToken cancellationToken)
        {
            var result = await _additionalCostService.AddAsync(additionalCost, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateAdditionalCost)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateAdditionalCost([FromBody] AdditionalCostDTO additionalCost, CancellationToken cancellationToken)
        {
            var result = await _additionalCostService.UpdateAsync(additionalCost, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteAdditionalCost)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteAdditionalCost(Guid additionalCostId, CancellationToken cancellationToken)
        {
            return await _additionalCostService.DeleteAsync(additionalCostId, true, true, true, cancellationToken);
        }

    }
}
