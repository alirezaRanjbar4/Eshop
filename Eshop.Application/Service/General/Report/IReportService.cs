using Eshop.Application.DTO.General.Report;

namespace Eshop.Application.Service.General.Report
{
    public interface IReportService 
    {
        Task<ReportChartDTO> GetReceiptCountChart(SearchReportChartDTO dto, CancellationToken cancellationToken);
        Task<ReportChartDTO> GetReceiptPriceChart(SearchReportChartDTO dto, CancellationToken cancellationToken);

        Task<ReportChartDTO> GetReceiptProductItemCountChart(SearchReportChartDTO dto, CancellationToken cancellationToken);
        Task<ReportChartDTO> GetReceiptProductItemPriceChart(SearchReportChartDTO dto, CancellationToken cancellationToken);

        Task<ReportChartDTO> GetReceiptServiceItemCountChart(SearchReportChartDTO dto, CancellationToken cancellationToken);
        Task<ReportChartDTO> GetReceiptServiceItemPriceChart(SearchReportChartDTO dto, CancellationToken cancellationToken);

        //Task<ReportChartDTO> GetPlateAreaChart(SearchReportChartDTO dTO, CancellationToken cancellationToken);
        //Task<ReportChartDTO> GetPlateDamageAreaChart(SearchReportChartDTO dTO, CancellationToken cancellationToken);
        //Task<ReportChartDTO> GetDesignerOrderRegisterCountChart(ReportTimeFrame chartReportTiming,CancellationToken cancellationToken);
        //Task<ReportChartDTO> GetDesignerOrderRegisterAreaChart(ReportTimeFrame chartReportTiming,CancellationToken cancellationToken);
        //Task<ReportChartDTO> GetAvrageKlischePrice(SearchReportChartDTO dto, CancellationToken cancellationToken);
        //Task<ReportChartDTO> GetKlischePrice(SearchReportChartDTO dto, CancellationToken cancellationToken);
        //Task<ReportChartDTO> GetAvrageKlischeArea(SearchReportChartDTO dto, CancellationToken cancellationToken);
        //Task<ReportChartDTO> GetCustomerOrderRegisterCountChart(ReportTimeFrame chartReportTiming, CancellationToken cancellationToken);
        //Task<ReportChartDTO> GetCustomerOrderRegisterAreaChart(ReportTimeFrame chartReportTiming, CancellationToken cancellationToken);
        //Task<ReportChartDTO> RatioInvoiceType(CancellationToken cancellationToken);
        //Task<ReportChartDTO> RatioOrderType(CancellationToken cancellationToken);
        List<YearDropdownDTO> GetYearsDropdown();
    }
}
