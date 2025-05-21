using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.SchedulerTask
{
    public class EditSchedulerTaskVendorTimeDTO : BaseDTO
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
    }
}
