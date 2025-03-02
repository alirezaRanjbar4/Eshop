using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Models;
using Eshop.Enum;
using Eshop.Service.Models.Order;
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
    [DisplayName("Order")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet(nameof(GetAllOrder))]
        public async Task<List<OrderDTO>> GetAllOrder(CancellationToken cancellationToken)
        {
            return await _orderService.GetAllAsync<OrderDTO>(null, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllOrderWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<OrderDTO>>> GetAllOrderWithTotal(BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _orderService.GetAllAsyncWithTotal<OrderDTO>(
                searchDTO,
                x => string.IsNullOrEmpty(searchDTO.SearchTerm) /*|| x.Name.Contains(searchDTO.SearchTerm)*/,
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpPost(nameof(AddOrder)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddOrder([FromBody] OrderDTO order, CancellationToken cancellationToken)
        {
            var result = await _orderService.AddAsync(order, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateOrder)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateOrder([FromBody] OrderDTO order, CancellationToken cancellationToken)
        {
            var result = await _orderService.UpdateAsync(order, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteOrder)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteOrder(Guid orderId, CancellationToken cancellationToken)
        {
            return await _orderService.DeleteAsync(orderId, true, true, true, cancellationToken);
        }

    }
}
