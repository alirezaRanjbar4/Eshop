﻿using Eshop.Application.DTO.Models.DemoRequest;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Models.Admin
{
    [Authorize]
    [DisplayName("DemoRequest")]
    public class AdminDemoRequestController : BaseController
    {
        private readonly IBaseService<DemoRequestEntity> _demoRequestService;
        public AdminDemoRequestController(IBaseService<DemoRequestEntity> demoRequestService)
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
