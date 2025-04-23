using Eshop.Application.DTO.Models.Store;
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

namespace Eshop.Presentation.Controllers.Models.Admin
{
    [Authorize]
    [DisplayName("AdminStore")]
    public class AdminStoreController : BaseController
    {
        private readonly IBaseService<StoreEntity> _storeService;
        public AdminStoreController(IBaseService<StoreEntity> storeService)
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
        public async Task<OperationResult<List<StoreDTO>>> GetAllStoreWithTotal([FromBody] BaseSearchDTO searchDTO, CancellationToken cancellationToken)
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
