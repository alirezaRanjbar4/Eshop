using Asp.Versioning;
using Eshop.Application.DTO.Models.Service;
using Eshop.Application.Service.Models.Service;
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

namespace Eshop.Presentation.Controllers.Models
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
        public async Task<List<SimpleServiceDTO>> GetAllService(CancellationToken cancellationToken)
        {
            return await _serviceService.GetAllAsync<SimpleServiceDTO>(
                x => x.StoreId == CurrentUserStoreId,
                i => i.Include(x => x.ServicePrices),
                o => o.OrderBy(x => x.Name),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(GetAllServiceWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<GetAllServiceDTO>>> GetAllServiceWithTotal([FromBody] BaseSearchDTO searchDTO, CancellationToken cancellationToken)
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
