using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.SchedulerTask
{
    public class SearchSchedulerTaskDTO
    {
        public List<Guid> VendorIds { get; set; }
        public string Date { get; set; }
        public string? SearchTerm { get; set; }
        public SchedulerTaskType? Type { get; set; }
    }
}
