using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.DTO.Models.Vendor;
using Eshop.Enum;
using Eshop.Service.Models.Vendor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public async Task<List<VendorUserDTO>> GetAllVendor(Guid storeId, CancellationToken cancellationToken)
        {
            //var storeId = User.FindFirst("StoreId") != null ? new Guid(User.FindFirst("StoreId").Value) : Guid.Empty;
            return await _vendorService.GetAllAsync<VendorUserDTO>(x => x.StoreId == storeId, i => i.Include(x => x.User), null, false, cancellationToken);
        }


        [HttpGet(nameof(GetVendor))]
        public async Task<VendorDTO> GetVendor(Guid id, CancellationToken cancellationToken)
        {
            return await _vendorService.GetAsync<VendorDTO>(x => x.Id == id, null, false, cancellationToken);
        }


        [HttpPost(nameof(AddVendor)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddVendor([FromBody] VendorUserDTO vendor, CancellationToken cancellationToken)
        {
            return await _vendorService.AddVendor(vendor, cancellationToken);
        }


        [HttpPost(nameof(UpdateVendor)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateVendor([FromBody] VendorUserDTO vendor, CancellationToken cancellationToken)
        {
            return await _vendorService.UpdateVendor(vendor, cancellationToken);
        }


        [HttpDelete(nameof(DeleteVendor)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteVendor(Guid vendorId, CancellationToken cancellationToken)
        {
            return await _vendorService.DeleteAsync(vendorId, true, true, true, cancellationToken);
        }

    }
}
