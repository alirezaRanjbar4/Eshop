using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.DTO.General;
using Eshop.DTO.Models.DemoRequest;
using Eshop.Service.Models.DemoRequest;
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
    [DisplayName("DemoRequest")]
    public class AdminDemoRequestController : BaseController
    {
        private readonly IDemoRequestService _demoRequestService;
        public AdminDemoRequestController(IDemoRequestService demoRequestService)
        {
            _demoRequestService = demoRequestService;
        }


        [HttpPost(nameof(GetAllDemoRequestWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<DemoRequestDTO>>> GetAllDemoRequestWithTotal([FromBody] BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _demoRequestService.GetAllAsyncWithTotal<DemoRequestDTO>(
                searchDTO,
                x => string.IsNullOrEmpty(searchDTO.SearchTerm) ||
                     x.Name.Contains(searchDTO.SearchTerm) ||
                     x.StoreName.Contains(searchDTO.SearchTerm),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(AddDemoRequest)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddDemoRequest([FromBody] DemoRequestDTO demoRequest, CancellationToken cancellationToken)
        {
            var result = await _demoRequestService.AddAsync(demoRequest, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateDemoRequest)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateDemoRequest([FromBody] DemoRequestDTO demoRequest, CancellationToken cancellationToken)
        {
            var result = await _demoRequestService.UpdateAsync(demoRequest, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteDemoRequest)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteDemoRequest(Guid demoRequestId, CancellationToken cancellationToken)
        {
            return await _demoRequestService.DeleteAsync(demoRequestId, true, true, true, cancellationToken);
        }

    }
}
