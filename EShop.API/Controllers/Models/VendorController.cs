using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.Core;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Models;
using Eshop.Enum;
using Eshop.Service.Models.Vendor;
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
    [DisplayName("Vendor")]
    public class VendorController : BaseController
    {
        private readonly IVendorService _vendorService;
        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }


        [HttpGet(nameof(GetAllVendor))]
        public async Task<List<VendorDTO>> GetAllVendor(CancellationToken cancellationToken)
        {
            return await _vendorService.GetAllAsync<VendorDTO>(null, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllVendorWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<VendorDTO>>> GetAllVendorWithTotal(BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _vendorService.GetAllAsyncWithTotal<VendorDTO>(
                searchDTO,
                x => string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Name.Contains(searchDTO.SearchTerm),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpPost(nameof(AddVendor)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddVendor([FromBody] VendorDTO vendor, CancellationToken cancellationToken)
        {
            var result = await _vendorService.AddAsync(vendor, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateVendor)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateVendor([FromBody] VendorDTO vendor, CancellationToken cancellationToken)
        {
            var result = await _vendorService.UpdateAsync(vendor, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteVendor)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteVendor(Guid vendorId, CancellationToken cancellationToken)
        {
            return await _vendorService.DeleteAsync(vendorId, true, true, true, cancellationToken);
        }

    }
}
