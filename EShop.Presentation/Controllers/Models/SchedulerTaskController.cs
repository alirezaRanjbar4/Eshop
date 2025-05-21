using Eshop.Application.DTO.Models.SchedulerTask;
using Eshop.Application.Service.Models.SchedulerTask;
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
    [DisplayName("SchedulerTask")]
    public class SchedulerTaskController : BaseController
    {
        private readonly ISchedulerTaskService _schedulerTaskService;
        public SchedulerTaskController(
             ISchedulerTaskService schedulerTaskService)
        {
            _schedulerTaskService = schedulerTaskService;
        }


        [HttpPost(nameof(GetSchedulerTask)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<List<CalenderSchedulerVM>> GetSchedulerTask([FromBody] SearchSchedulerTaskDTO searchDto, CancellationToken cancellationToken)
        {
            return await _schedulerTaskService.GetSchedulerTask(searchDto, cancellationToken);
        }


        [HttpGet(nameof(GetSingleSchedulerTask))/*, DisplayName(nameof(PermissionResourceEnums.SingleViewPermission))*/]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<SchedulerTaskDTO> GetSingleSchedulerTask(Guid id, CancellationToken cancellationToken)
        {
            return await _schedulerTaskService.GetAsync<SchedulerTaskDTO>(
                x => x.Id == id,
                i => i.Include(x => x.AssignedTo),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(AddSchedulerTask)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddSchedulerTask([FromBody] SchedulerTaskDTO schedulerTaskDTO, CancellationToken cancellationToken)
        {
            return await _schedulerTaskService.AddSchedulerTask(schedulerTaskDTO, cancellationToken);
        }


        [HttpPost(nameof(EditSchedulerTask)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> EditSchedulerTask([FromBody] SchedulerTaskDTO schedulerTaskDTO, CancellationToken cancellationToken)
        {
            return await _schedulerTaskService.EditSchedulerTask(schedulerTaskDTO, cancellationToken);
        }


        [HttpPost(nameof(DeleteSchedulerTask)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteSchedulerTask([FromBody] SchedulerTaskDTO schedulerTaskDTO, CancellationToken cancellationToken)
        {
            return await _schedulerTaskService.DeleteSchedulerTask(schedulerTaskDTO, cancellationToken);
        }
    }
}
