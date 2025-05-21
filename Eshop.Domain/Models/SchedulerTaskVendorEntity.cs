using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class SchedulerTaskVendorEntity : BaseTrackedModel, IBaseEntity
    {
        public bool IsDone { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public DateTime? ReminderDateTime { get; set; }
        public string? Description { get; set; }

        public Guid SchedulerTaskId { get; set; }
        public virtual SchedulerTaskEntity SchedulerTask { get; set; }

        public Guid VendorId { get; set; }
        public virtual VendorEntity Vendor { get; set; }
    }
}