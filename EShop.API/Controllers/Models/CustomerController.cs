using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.DTO.Models.Customer;
using Eshop.DTO.Models.Vendor;
using Eshop.Enum;
using Eshop.Service.Models.Customer;
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
    [DisplayName("Customer")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet(nameof(GetAllCustomer))]
        public async Task<List<CustomerDTO>> GetAllCustomer(Guid storeId, CancellationToken cancellationToken)
        {
            return await _customerService.GetAllAsync<CustomerDTO>(x => x.StoreId == storeId, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllCustomerWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<CustomerDTO>>> GetAllCustomerWithTotal(BaseSearchByIdDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _customerService.GetAllAsyncWithTotal<CustomerDTO>(
            searchDTO,
                x => x.StoreId == searchDTO.Id,
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(AddCustomer)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddCustomer([FromBody] CustomerDTO customer, CancellationToken cancellationToken)
        {
            //var storeId = User.FindFirst("StoreId") != null ? new Guid(User.FindFirst("StoreId").Value) : Guid.Empty;
            var result = await _customerService.AddAsync(customer, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateCustomer)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateCustomer([FromBody] CustomerDTO customer, CancellationToken cancellationToken)
        {
            var result = await _customerService.UpdateAsync(customer, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteCustomer)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteCustomer(Guid customerId, CancellationToken cancellationToken)
        {
            return await _customerService.DeleteAsync(customerId, true, true, true, cancellationToken);
        }

    }
}
