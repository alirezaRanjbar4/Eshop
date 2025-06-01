using Eshop.Share.Enum;

namespace Eshop.Application.DTO.General.Report
{
    public class SearchReportChartDTO
    {
        public ReportTimeFrame ChartReportTiming { get; set; }
        public Guid StoreId { get; set; }
        public Guid VendorId { get; set; }
        public Guid AccountPartyId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ServiceId { get; set; }
        public int Year { get; set; }
    }
}
