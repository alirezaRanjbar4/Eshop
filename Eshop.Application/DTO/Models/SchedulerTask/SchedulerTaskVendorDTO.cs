using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.SchedulerTask
{
    public class SchedulerTaskVendorDTO : BaseDTO
    {
        public bool IsDone { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public DateTime? ReminderDateTime { get; set; }
        public string? Description { get; set; }
        public Guid SchedulerTaskId { get; set; }
        public Guid VendorId { get; set; }
        public Guid FirstPersonId { get; set; }
    }
}
