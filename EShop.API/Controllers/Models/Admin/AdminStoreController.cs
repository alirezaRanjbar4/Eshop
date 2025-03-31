using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Models.Store;
using Eshop.Service.Models.Store;
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
    [DisplayName("AdminStore")]
    public class AdminStoreController : BaseController
    {
        private readonly IStoreService _storeService;
        public AdminStoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }


        [HttpGet(nameof(GetAllStore))]
        public async Task<List<StoreDTO>> GetAllStore(CancellationToken cancellationToken)
        {
            return await _storeService.GetAllAsync<StoreDTO>(null, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllStoreWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<StoreDTO>>> GetAllStoreWithTotal(BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _storeService.GetAllAsyncWithTotal<StoreDTO>(
                searchDTO,
                x => string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Name.Contains(searchDTO.SearchTerm),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpPost(nameof(AddStore)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddStore([FromBody] StoreDTO store, CancellationToken cancellationToken)
        {
            var result = await _storeService.AddAsync(store, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateStore)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateStore([FromBody] StoreDTO store, CancellationToken cancellationToken)
        {
            var result = await _storeService.UpdateAsync(store, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteStore)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteStore(Guid storeId, CancellationToken cancellationToken)
        {
            return await _storeService.DeleteAsync(storeId, true, true, true, cancellationToken);
        }

    }
}
