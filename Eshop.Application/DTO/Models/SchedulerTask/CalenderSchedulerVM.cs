namespace Eshop.Application.DTO.Models.SchedulerTask
{
    public class CalenderSchedulerVM
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public int Day { get; set; }
        public bool IsActive { get; set; }
        public bool IsToday { get; set; }
        public DateOnly Date { get; set; }
        public List<CalenderSchedulerItemVM> Items { get; set; }
    }
}
