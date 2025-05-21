using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.SchedulerTask
{
    public class SchedulerTaskVendorReminderDTO : BaseDTO
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public Guid SchedulerTaskId { get; set; }
    }
}
