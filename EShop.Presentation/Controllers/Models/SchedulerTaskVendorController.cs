using Eshop.Application.DTO.Models.SchedulerTask;
using Eshop.Application.Service.Models.SchedulerTask;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Models
{
    [Authorize]
    [DisplayName("SchedulerTaskVendor")]
    public class SchedulerTaskVendorController : BaseController
    {
        private readonly ISchedulerTaskVendorService _schedulerTaskVendorService;
        public SchedulerTaskVendorController(
             ISchedulerTaskVendorService schedulerTaskVendorService)
        {
            _schedulerTaskVendorService = schedulerTaskVendorService;
        }


        [HttpGet(nameof(GetSingleSchedulerTaskVendor)), DisplayName(nameof(PermissionResourceEnums.GetPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<SchedulerTaskVendorDTO> GetSingleSchedulerTaskVendor(Guid id, CancellationToken cancellationToken)
        {
            return await _schedulerTaskVendorService.GetAsync<SchedulerTaskVendorDTO>(
                x => x.Id == id,
                null,
                false,
                cancellationToken);
        }


        [HttpPost(nameof(ChangeTime))]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> ChangeTime([FromBody] EditSchedulerTaskVendorTimeDTO schedulerTaskDTO, CancellationToken cancellationToken)
        {
            return await _schedulerTaskVendorService.ChangeTime(schedulerTaskDTO, cancellationToken);
        }


        [HttpPost(nameof(ChangeVendor))]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> ChangeVendor([FromBody] EditSchedulerTaskVendorVendorIdDTO schedulerTaskDTO, CancellationToken cancellationToken)
        {
            return await _schedulerTaskVendorService.ChangeVendor(schedulerTaskDTO, cancellationToken);
        }
       
        [HttpPost(nameof(FinishTask))]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> FinishTask([FromBody] Guid id, CancellationToken cancellationToken)
        {
            return await _schedulerTaskVendorService.FinishTask(id, cancellationToken);
        }


        [HttpPost(nameof(EditSchedulerTaskVendor))]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> EditSchedulerTaskVendor([FromBody] SchedulerTaskVendorDTO schedulerTaskDTO, CancellationToken cancellationToken)
        {
            return await _schedulerTaskVendorService.EditSchedulerTaskVendor(schedulerTaskDTO, cancellationToken);
        }

    }
}
