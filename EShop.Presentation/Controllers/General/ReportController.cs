using Eshop.Application.DTO.General.Report;
using Eshop.Application.Service.General.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.General
{
    [Authorize]
    [DisplayName("Report")]
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;

        public ReportController(
            IReportService reportService)
        {
            _reportService = reportService;
        }


        #region چارت گزارش رسید ها
        [HttpPost(nameof(GetReceiptCountChart)), /*DisplayName(nameof(PermissionResourceEnums.GetOrderCountChart))*/]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ReportChartDTO> GetReceiptCountChart([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            dto.StoreId = CurrentUserStoreId;
            return await _reportService.GetReceiptCountChart(dto, cancellationToken);
        }
        [HttpPost(nameof(GetReceiptPriceChart))]
        public async Task<ReportChartDTO> GetReceiptPriceChart([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            dto.StoreId = CurrentUserStoreId;
            return await _reportService.GetReceiptPriceChart(dto, cancellationToken);
        }
        #endregion

        #region چارت گزارش آیتم محصول رسید ها
        [HttpPost(nameof(GetReceiptProductItemCountChart)), /*DisplayName(nameof(PermissionResourceEnums.GetOrderCountChart))*/]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ReportChartDTO> GetReceiptProductItemCountChart([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            dto.StoreId = CurrentUserStoreId;
            return await _reportService.GetReceiptProductItemCountChart(dto, cancellationToken);
        }
        [HttpPost(nameof(GetReceiptProductItemPriceChart))]
        public async Task<ReportChartDTO> GetReceiptProductItemPriceChart([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            dto.StoreId = CurrentUserStoreId;
            return await _reportService.GetReceiptProductItemPriceChart(dto, cancellationToken);
        }
        #endregion

        #region چارت گزارش آیتم سرویس رسید ها
        [HttpPost(nameof(GetReceiptServiceItemCountChart)), /*DisplayName(nameof(PermissionResourceEnums.GetOrderCountChart))*/]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ReportChartDTO> GetReceiptServiceItemCountChart([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            dto.StoreId = CurrentUserStoreId;
            return await _reportService.GetReceiptServiceItemCountChart(dto, cancellationToken);
        }
        [HttpPost(nameof(GetReceiptServiceItemPriceChart))]
        public async Task<ReportChartDTO> GetReceiptServiceItemPriceChart([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            dto.StoreId = CurrentUserStoreId;
            return await _reportService.GetReceiptServiceItemPriceChart(dto, cancellationToken);
        }
        #endregion

        //#region چارت گزارش طراحان
        //[HttpPost(nameof(GetDesignerOrderRegisterCountChart)), DisplayName(nameof(PermissionResourceEnums.GetDesignerOrderRegisterCountChart))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        //public async Task<ReportChartDTO> GetDesignerOrderRegisterCountChart([FromBody] ReportTimeFrame chartReportTiming, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetDesignerOrderRegisterCountChart(chartReportTiming, cancellationToken);
        //}

        //[HttpPost(nameof(GetDesignerOrderRegisterAreaChart))]
        //public async Task<ReportChartDTO> GetDesignerOrderRegisterAreaChart([FromBody] ReportTimeFrame chartReportTiming, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetDesignerOrderRegisterAreaChart(chartReportTiming, cancellationToken);
        //}
        //#endregion

        //#region چارت گزارش مشتریان 
        //[HttpPost(nameof(GetCustomerOrderRegisterCountChart)), DisplayName(nameof(PermissionResourceEnums.GetCustomerOrderRegisterCountChart))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        //public async Task<ReportChartDTO> GetCustomerOrderRegisterCountChart([FromBody] ReportTimeFrame chartReportTiming, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetCustomerOrderRegisterCountChart(chartReportTiming, cancellationToken);
        //}
        //[HttpPost(nameof(GetCustomerOrderRegisterAreaChart))]
        //public async Task<ReportChartDTO> GetCustomerOrderRegisterAreaChart([FromBody] ReportTimeFrame chartReportTiming, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetCustomerOrderRegisterAreaChart(chartReportTiming, cancellationToken);
        //}
        //#endregion

        //#region چارت گزارش قیمت فروش
        //[HttpPost(nameof(GetAvrageKlischePrice)), DisplayName(nameof(PermissionResourceEnums.GetAvrageKlischePrice))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        //public async Task<ReportChartDTO> GetAvrageKlischePrice([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetAvrageKlischePrice(dto, cancellationToken);
        //}
        //[HttpPost(nameof(GetAvrageKlischeArea))]
        //public async Task<ReportChartDTO> GetAvrageKlischeArea([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetAvrageKlischeArea(dto, cancellationToken);
        //}
        //[HttpPost(nameof(GetKlischePrice))]
        //public async Task<ReportChartDTO> GetKlischePrice([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetKlischePrice(dto, cancellationToken);
        //}
        //#endregion

        //#region چارت گزارش تولید
        //[HttpPost(nameof(GetPlateAreaChart)), DisplayName(nameof(PermissionResourceEnums.GetPlateCountChart))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        //public async Task<ReportChartDTO> GetPlateAreaChart([FromBody] SearchReportChartDTO dTO, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetPlateAreaChart(dTO, cancellationToken);
        //}
        //#endregion

        //#region چارت گزارش خرابی تولید
        //[HttpPost(nameof(GetPlateDamageAreaChart)), DisplayName(nameof(PermissionResourceEnums.GetPlateDamageChart))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        //public async Task<ReportChartDTO> GetPlateDamageAreaChart([FromBody] SearchReportChartDTO dTO, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetPlateDamageAreaChart(dTO, cancellationToken);
        //}
        //#endregion

        //#region چارت تعداد و مساحت سفارش برای طراح
        //[HttpPost(nameof(GetDesignerOrderCountChart)), DisplayName(nameof(PermissionResourceEnums.GetDesignerOrderCountChart))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        //public async Task<ReportChartDTO> GetDesignerOrderCountChart([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetOrderCountChart(dto, cancellationToken);
        //}
        //[HttpPost(nameof(GetDesignerOrderAreaChart))]
        //public async Task<ReportChartDTO> GetDesignerOrderAreaChart([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetOrderAreaChart(dto, cancellationToken);
        //}
        //#endregion

        //#region چارت تعداد و مساحت سفارش برای مشتری
        //[HttpPost(nameof(GetCustomerOrderCountChart)), DisplayName(nameof(PermissionResourceEnums.GetCustomerOrderCountChart))]
        ////[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        //public async Task<ReportChartDTO> GetCustomerOrderCountChart([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetOrderCountChart(dto, cancellationToken);
        //}
        //[HttpPost(nameof(GetCustomerOrderAreaChart))]
        //public async Task<ReportChartDTO> GetCustomerOrderAreaChart([FromBody] SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    return await _dashboardService.GetOrderAreaChart(dto, cancellationToken);
        //}
        //#endregion

        #region مقادیر مشترک
        //[HttpGet(nameof(GetAllKlischeType))]
        //public async Task<List<SimpleKlischeTypeDTO>> GetAllKlischeType(Guid klischeThiknessId, CancellationToken cancellationToken)
        //{
        //    return await _klischeTypeService.GetAllAsync<SimpleKlischeTypeDTO>(
        //        x => klischeThiknessId == Guid.Empty || x.KlischeTypeAndThicknesses.Any(z => z.KlischeThicknessId == klischeThiknessId),
        //        i => i.Include(x => x.KlischeTypeAndThicknesses),
        //        null,
        //        false,
        //        cancellationToken);
        //}
        //[HttpGet(nameof(GetAllKlischeThikness))]
        //public async Task<List<SimpleKlischeThicknessDTO>> GetAllKlischeThikness(Guid klischeTypeId, CancellationToken cancellationToken)
        //{
        //    return await _klischeThicknessService.GetAllAsync<SimpleKlischeThicknessDTO>(
        //        x => klischeTypeId == Guid.Empty || x.KlischeTypeAndThicknesses.Any(z => z.KlischeTypeId == klischeTypeId),
        //        i => i.Include(x => x.KlischeTypeAndThicknesses),
        //        null,
        //        false,
        //        cancellationToken);
        //}
        [HttpGet(nameof(GetYearsDropdown))]
        public List<YearDropdownDTO> GetYearsDropdown()
        {
            return _reportService.GetYearsDropdown();
        }
        #endregion
    }
}
