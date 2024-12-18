using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.Core;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Models;
using Eshop.Enum;
using Eshop.Service.Models.ShoppingCardItem;
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
    [DisplayName("ShoppingCard")]
    public class ShoppingCardItemController : BaseController
    {
        private readonly IShoppingCardItemService _shoppingCardItemService;
        public ShoppingCardItemController(IShoppingCardItemService shoppingCardItemService)
        {
            _shoppingCardItemService = shoppingCardItemService;
        }


        [HttpGet(nameof(GetAllShoppingCardItem))]
        public async Task<List<ShoppingCardItemDTO>> GetAllShoppingCardItem(CancellationToken cancellationToken)
        {
            return await _shoppingCardItemService.GetAllAsync<ShoppingCardItemDTO>(null, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllShoppingCardItemWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<ShoppingCardItemDTO>>> GetAllShoppingCardItemWithTotal(BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _shoppingCardItemService.GetAllAsyncWithTotal<ShoppingCardItemDTO>(
                searchDTO,
                x => string.IsNullOrEmpty(searchDTO.SearchTerm) /*|| x.Name.Contains(searchDTO.SearchTerm)*/,
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpPost(nameof(AddShoppingCardItem)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddShoppingCardItem([FromBody] ShoppingCardItemDTO shoppingCardItem, CancellationToken cancellationToken)
        {
            var result = await _shoppingCardItemService.AddAsync(shoppingCardItem, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateShoppingCardItem)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateShoppingCardItem([FromBody] ShoppingCardItemDTO shoppingCardItem, CancellationToken cancellationToken)
        {
            var result = await _shoppingCardItemService.UpdateAsync(shoppingCardItem, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteShoppingCardItem)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteShoppingCardItem(Guid shoppingCardItemId, CancellationToken cancellationToken)
        {
            return await _shoppingCardItemService.DeleteAsync(shoppingCardItemId, true, true, true, cancellationToken);
        }

    }
}
