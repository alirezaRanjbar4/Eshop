using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Models;
using Eshop.Enum;
using Eshop.Service.Models.Warehouse;
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
    [DisplayName("Warehouse")]
    public class WarehouseController : BaseController
    {
        private readonly IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }


        [HttpGet(nameof(GetAllWarehouse))]
        public async Task<List<WarehouseDTO>> GetAllWarehouse(Guid storeId, CancellationToken cancellationToken)
        {
            return await _warehouseService.GetAllAsync<WarehouseDTO>(x => x.StoreId == storeId, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllWarehouseWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<WarehouseDTO>>> GetAllWarehouseWithTotal(BaseSearchByIdDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _warehouseService.GetAllAsyncWithTotal<WarehouseDTO>(
                searchDTO,
                x => x.StoreId == searchDTO.Id && (string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Name.Contains(searchDTO.SearchTerm)),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpGet(nameof(GetWarehouseInventory))]
        public async Task<List<WarehouseInventoryDTO>> GetWarehouseInventory(Guid warehouseId, CancellationToken cancellationToken)
        {
            return await _warehouseService.GetWarehouseInventory(warehouseId, cancellationToken);
        }


        [HttpPost(nameof(AddWarehouse)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddWarehouse([FromBody] WarehouseDTO warehouse, CancellationToken cancellationToken)
        {
            var result = await _warehouseService.AddAsync(warehouse, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateWarehouse)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateWarehouse([FromBody] WarehouseDTO warehouse, CancellationToken cancellationToken)
        {
            var result = await _warehouseService.UpdateAsync(warehouse, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteWarehouse)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteWarehouse(Guid warehouseId, CancellationToken cancellationToken)
        {
            return await _warehouseService.DeleteAsync(warehouseId, true, true, true, cancellationToken);
        }

    }
}
