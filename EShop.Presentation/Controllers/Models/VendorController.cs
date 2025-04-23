using Eshop.Application.DTO.Models.Vendor;
using Eshop.Application.Service.Models.Vendor;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Models
{
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
        public async Task<List<VendorUserDTO>> GetAllVendor(CancellationToken cancellationToken)
        {
            return await _vendorService.GetAllAsync<VendorUserDTO>(
                x => x.StoreId == CurrentUserStoreId,
                i => i.Include(x => x.User),
                null,
                false,
                cancellationToken);
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
