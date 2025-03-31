using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.DTO.General;
using Eshop.DTO.Models.Service;
using Eshop.Service.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [DisplayName("Service")]
    public class ServiceController : BaseController
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }


        [HttpGet(nameof(GetAllService))]
        public async Task<List<ServiceDTO>> GetAllService(CancellationToken cancellationToken)
        {
            return await _serviceService.GetAllAsync<ServiceDTO>(x => x.StoreId == CurrentUserStoreId, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllServiceWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<GetAllServiceDTO>>> GetAllServiceWithTotal(BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _serviceService.GetAllAsyncWithTotal<GetAllServiceDTO>(
                searchDTO,
                x => x.StoreId == CurrentUserStoreId && (string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Name.Contains(searchDTO.SearchTerm)),
                i => i.Include(x => x.ServiceCategories).ThenInclude(x => x.Category)
                      .Include(x => x.ServicePrices),
                o => o.OrderByDescending(x => x.CreateDate),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(GetService)), DisplayName(nameof(PermissionResourceEnums.GetPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<GetServiceDTO> GetService(Guid serviceId, CancellationToken cancellationToken)
        {
            return await _serviceService.GetAsync<GetServiceDTO>(
                x => x.Id == serviceId && x.StoreId == CurrentUserStoreId,
                i => i.Include(x => x.ServiceCategories)
                      .Include(x => x.ServicePrices)
                      .Include(x => x.Images),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(AddService)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddService([FromBody] ServiceDTO service, CancellationToken cancellationToken)
        {
            return await _serviceService.AddService(service, cancellationToken);
        }


        [HttpPost(nameof(UpdateService)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateService([FromBody] ServiceDTO service, CancellationToken cancellationToken)
        {
            return await _serviceService.UpdateService(service, cancellationToken);
        }


        [HttpDelete(nameof(DeleteService)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteService(Guid serviceId, CancellationToken cancellationToken)
        {
            return await _serviceService.DeleteAsync(serviceId, true, true, true, cancellationToken);
        }

    }
}
