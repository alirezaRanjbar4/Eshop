namespace Eshop.Application.DTO.General.Report
{
    public class ReportChartDTO
    {
        public ReportChartDTO()
        {
            Items = new List<ReportChartItemDTO>();
        }
        public string Title { get; set; }
        public decimal Total1 { get; set; }
        public decimal Total2 { get; set; }
        public decimal Total3 { get; set; }
        public decimal Total4 { get; set; }
        
        public List<ReportChartItemDTO> Items { get; set; }
    }

    public class ReportChartItemDTO
    {
        public string Name { get; set; }
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal Value3 { get; set; }
        public decimal Value4 { get; set; }
    }
}
