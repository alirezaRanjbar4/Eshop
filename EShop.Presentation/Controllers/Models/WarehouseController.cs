using Asp.Versioning;
using Eshop.Application.DTO.Models.Warehouse;
using Eshop.Application.Service.Models.Warehouse;
using Eshop.Presentation.Components;
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
        public async Task<List<WarehouseDTO>> GetAllWarehouse(CancellationToken cancellationToken)
        {
            return await _warehouseService.GetAllAsync<WarehouseDTO>(x => x.StoreId == CurrentUserStoreId, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllWarehouseWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<WarehouseDTO>>> GetAllWarehouseWithTotal([FromBody] BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _warehouseService.GetAllAsyncWithTotal<WarehouseDTO>(
                searchDTO,
                x => x.StoreId == CurrentUserStoreId && (string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Name.Contains(searchDTO.SearchTerm)),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpGet(nameof(GetWarehouse))]
        public async Task<AddWarehouseDTO> GetWarehouse(Guid warehouseId, CancellationToken cancellationToken)
        {
            return await _warehouseService.GetAsync<AddWarehouseDTO>(x => x.Id == warehouseId && x.StoreId == CurrentUserStoreId, i => i.Include(x => x.WarehouseLocations), false, cancellationToken);
        }


        [HttpGet(nameof(GetWarehouseInventory))]
        public async Task<List<WarehouseInventoryDTO>> GetWarehouseInventory(Guid warehouseId, CancellationToken cancellationToken)
        {
            return await _warehouseService.GetWarehouseInventory(warehouseId, cancellationToken);
        }


        [HttpPost(nameof(AddWarehouse)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddWarehouse([FromBody] AddWarehouseDTO warehouse, CancellationToken cancellationToken)
        {
            var result = await _warehouseService.AddAsync(warehouse, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateWarehouse)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateWarehouse([FromBody] AddWarehouseDTO warehouse, CancellationToken cancellationToken)
        {
            return await _warehouseService.UpdateWarehouse(warehouse, cancellationToken);
        }


        [HttpDelete(nameof(DeleteWarehouse)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteWarehouse(Guid warehouseId, CancellationToken cancellationToken)
        {
            return await _warehouseService.DeleteAsync(warehouseId, true, true, true, cancellationToken);
        }

    }
}
