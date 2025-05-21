using Eshop.Application.DTO.Models.SchedulerTask;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Application.Service.Models.SchedulerTask
{
    public interface ISchedulerTaskService : IBaseService<SchedulerTaskEntity>, IScopedDependency
    {
        Task<List<CalenderSchedulerVM>> GetSchedulerTask([FromBody] SearchSchedulerTaskDTO searchDto, CancellationToken cancellationToken);
        Task<bool> AddSchedulerTask(SchedulerTaskDTO schedulerTaskDTO, CancellationToken cancellationToken);
        Task<bool> EditSchedulerTask(SchedulerTaskDTO schedulerTaskDTO, CancellationToken cancellationToken);
        Task<bool> DeleteSchedulerTask(SchedulerTaskDTO schedulerTaskPersonDTO, CancellationToken cancellationToken);

    }
}
