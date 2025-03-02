using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Models;
using Eshop.Enum;
using Eshop.Service.Models.WarehouseLocation;
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
    [DisplayName("WarehouseLocation")]
    public class WarehouseLocationController : BaseController
    {
        private readonly IWarehouseLocationService _warehouseService;
        public WarehouseLocationController(IWarehouseLocationService warehouseService)
        {
            _warehouseService = warehouseService;
        }


        [HttpGet(nameof(GetAllWarehouseLocation))]
        public async Task<List<WarehouseLocationDTO>> GetAllWarehouseLocation(CancellationToken cancellationToken)
        {
            return await _warehouseService.GetAllAsync<WarehouseLocationDTO>(null, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllWarehouseLocationWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<WarehouseLocationDTO>>> GetAllWarehouseLocationWithTotal(BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _warehouseService.GetAllAsyncWithTotal<WarehouseLocationDTO>(
                searchDTO,
                x => string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Name.Contains(searchDTO.SearchTerm),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, 
                cancellationToken);
        }


        [HttpPost(nameof(AddWarehouseLocation)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddWarehouseLocation([FromBody] WarehouseLocationDTO warehouse, CancellationToken cancellationToken)
        {
            var result = await _warehouseService.AddAsync(warehouse, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateWarehouseLocation)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateWarehouseLocation([FromBody] WarehouseLocationDTO warehouse, CancellationToken cancellationToken)
        {
            var result = await _warehouseService.UpdateAsync(warehouse, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteWarehouseLocation)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteWarehouseLocation(Guid warehouseId, CancellationToken cancellationToken)
        {
            return await _warehouseService.DeleteAsync(warehouseId, true, true, true, cancellationToken);
        }

    }
}
