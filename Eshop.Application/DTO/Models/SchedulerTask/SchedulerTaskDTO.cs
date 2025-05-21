using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.SchedulerTask
{
    public class SchedulerTaskDTO : BaseDTO
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

        public List<Guid> Vendors { get; set; }
    }
}
