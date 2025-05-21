using Eshop.Domain.General;
using Eshop.Share.Enum;

namespace Eshop.Domain.Models
{
    public class SchedulerTaskEntity : BaseTrackedModel, IBaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }

        public SchedulerTaskPriority Priority { get; set; }
        public SchedulerTaskType Type { get; set; }
        public SchedulerTaskRepetType? RepetType { get; set; }

        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public DateTime? ReminderDateTime { get; set; } 

        public int RepeatCount { get; set; }

        public Guid? RelatedId { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<SchedulerTaskVendorEntity> AssignedTo { get; set; }
    }
}