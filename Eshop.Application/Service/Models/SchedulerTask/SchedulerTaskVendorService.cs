using AutoMapper;
using Eshop.Application.DTO.Models.SchedulerTask;
using Eshop.Application.Service.General;
using Eshop.Application.Service.Identity.User;
using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;

namespace Eshop.Application.Service.Models.SchedulerTask
{
    public class SchedulerTaskVendorService : BaseService<SchedulerTaskVendorEntity>, ISchedulerTaskVendorService
    {
        private readonly IUserService _userService;
        public SchedulerTaskVendorService(
            IMapper mapper,
            IBaseRepository<SchedulerTaskVendorEntity> schedulerTaskVendorRepository,
            IUserService userService) : base(schedulerTaskVendorRepository, mapper)
        {
            _userService = userService;
        }

        public async Task<bool> ChangeVendor(EditSchedulerTaskVendorVendorIdDTO dto, CancellationToken cancellationToken)
        {
            var schedulerTaskVendor = await GetAsync<SchedulerTaskVendorDTO>(x => x.Id == dto.Id, null, false, cancellationToken);
            if (schedulerTaskVendor == null)
                return false;

            var previousVendorId = schedulerTaskVendor.VendorId;

            schedulerTaskVendor.VendorId = dto.VendorId;
            var result = await UpdateAsync(schedulerTaskVendor, true, true, cancellationToken);


            return result != null;
        }

        public async Task<bool> ChangeTime(EditSchedulerTaskVendorTimeDTO dto, CancellationToken cancellationToken)
        {
            var schedulerTaskVendor = await GetAsync<SchedulerTaskVendorDTO>(x => x.Id == dto.Id, null, false, cancellationToken);
            if (schedulerTaskVendor == null)
                return false;

            var previousDate = schedulerTaskVendor.Date;
            var previousTime = schedulerTaskVendor.Time;

            schedulerTaskVendor.Time = dto.Time;
            schedulerTaskVendor.Date = dto.Date;
            var result = await UpdateAsync(schedulerTaskVendor, true, true, cancellationToken);

            return result != null;

        }

        public async Task<bool> FinishTask(Guid id, CancellationToken cancellationToken)
        {
            var schedulerTaskVendor = await GetAsync<SchedulerTaskVendorDTO>(x => x.Id == id, null, false, cancellationToken);
            if (schedulerTaskVendor == null)
                return false;

            schedulerTaskVendor.IsDone = true;
            var result = await UpdateAsync(schedulerTaskVendor, true, true, cancellationToken);

            return result != null;
        }

        public async Task<bool> EditSchedulerTaskVendor(SchedulerTaskVendorDTO dto, CancellationToken cancellationToken)
        {
            var schedulerTaskVendor = await GetAsync<SchedulerTaskVendorDTO>(x => x.Id == dto.Id, null, false, cancellationToken);
            if (schedulerTaskVendor == null)
                return false;

            var previousVendorId = schedulerTaskVendor.VendorId;
            var previousDate = schedulerTaskVendor.Date;
            var previousTime = schedulerTaskVendor.Time;

            schedulerTaskVendor = _mapper.Map(dto, schedulerTaskVendor);

            var result = await UpdateAsync(schedulerTaskVendor, true, true, cancellationToken);

            return result != null;
        }

    }
}
