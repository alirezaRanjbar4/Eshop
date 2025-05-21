using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.SchedulerTask
{
    public class CalenderSchedulerItemVM 
    {
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public SchedulerTaskPriority Priority { get; set; }
        public bool IsDone { get; set; }
        public Guid SchedulerTaskId { get; set; }
    }
}
