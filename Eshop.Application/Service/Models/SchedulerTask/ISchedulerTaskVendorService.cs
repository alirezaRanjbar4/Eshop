using Eshop.Application.DTO.Models.SchedulerTask;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Application.Service.Models.SchedulerTask
{
    public interface ISchedulerTaskVendorService : IBaseService<SchedulerTaskVendorEntity>, IScopedDependency
    {
        Task<bool> ChangeTime(EditSchedulerTaskVendorTimeDTO schedulerTaskDTO, CancellationToken cancellationToken);
        Task<bool> ChangeVendor(EditSchedulerTaskVendorVendorIdDTO schedulerTaskDTO, CancellationToken cancellationToken);
        Task<bool> FinishTask(Guid id, CancellationToken cancellationToken);
        Task<bool> EditSchedulerTaskVendor(SchedulerTaskVendorDTO schedulerTaskDTO, CancellationToken cancellationToken);

    }
}
