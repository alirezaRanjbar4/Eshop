using Asp.Versioning;
using Eshop.Application.DTO.Models.Vendor;
using Eshop.Application.Service.Models.Vendor;
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

namespace Eshop.Presentation.Controllers.Models.Admin
{
    [ApiVersion(VersionProperties.V1)]
    [Authorize]
    [DisplayName("AdminVendor")]
    public class AdminVendorController : BaseController
    {
        private readonly IVendorService _vendorService;
        public AdminVendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }


        [HttpPost(nameof(GetAllVendorWithTotal))]
        public async Task<OperationResult<List<VendorUserDTO>>> GetAllVendorWithTotal([FromBody] BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _vendorService.GetAllAsyncWithTotal<VendorUserDTO>(
                searchDTO,
                x => string.IsNullOrEmpty(searchDTO.SearchTerm) ||
                     x.Name.Contains(searchDTO.SearchTerm) ||
                     x.Store != null && x.Store.Name.Contains(searchDTO.SearchTerm) ||
                     x.User != null && x.User.UserName.Contains(searchDTO.SearchTerm),
                i => i.Include(x => x.User).Include(x => x.Store),
                o => o.OrderByDescending(x => x.CreateDate),
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
