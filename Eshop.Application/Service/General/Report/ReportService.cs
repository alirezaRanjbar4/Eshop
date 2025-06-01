using Eshop.Application.DTO.General.Report;
using Eshop.Application.DTO.Models.Receipt;
using Eshop.Application.Service.Models.Receipt;
using Eshop.Domain.Models;
using Eshop.Share.Enum;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Eshop.Application.Service.General.Report
{
    public class ReportService : IReportService
    {
        private readonly IReceiptService _receiptService;
        private readonly IBaseService<ReceiptProductItemEntity> _receiptProductItemService;
        private readonly IBaseService<ReceiptServiceItemEntity> _receiptServiceItemService;

        public ReportService(
            IReceiptService receiptService,
            IBaseService<ReceiptProductItemEntity> receiptProductItemService,
            IBaseService<ReceiptServiceItemEntity> receiptServiceItemService)
        {
            _receiptService = receiptService;
            _receiptProductItemService = receiptProductItemService;
            _receiptServiceItemService = receiptServiceItemService;
        }

        public async Task<ReportChartDTO> GetReceiptCountChart(SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            var result = new ReportChartDTO();
            var persianCalendar = new PersianCalendar();
            DateTime today = DateTime.Today;

            // Get the current Persian year, month
            int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
            int persianMonth = persianCalendar.GetMonth(today);

            if (dto.ChartReportTiming == ReportTimeFrame.Daily)
            {
                DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
                int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

                var allReceipts = await _receiptService.GetAllAsync<ReportReceiptDTO>(
                    x => x.Date >= firstDayOfMonth &&
                         x.StoreId == dto.StoreId &&
                         //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                         (dto.AccountPartyId == Guid.Empty || x.AccountPartyId == dto.AccountPartyId),
                    null,
                    null,
                    false,
                    cancellationToken);

                for (int day = 1; day <= lastDayInMonth; day++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

                    item.Name = $"{day: 00}";
                    item.Value1 = allReceipts.Count(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Enter);
                    item.Value2 = allReceipts.Count(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Exit);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
            {
                string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
                DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);

                var allReceipts = await _receiptService.GetAllAsync<ReportReceiptDTO>(
                     x => x.Date >= currentYear &&
                          x.StoreId == dto.StoreId &&
                          //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                          (dto.AccountPartyId == Guid.Empty || x.AccountPartyId == dto.AccountPartyId),
                     null,
                     null,
                     false,
                     cancellationToken);

                for (int month = 1; month <= 12; month++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
                    var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
                    DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

                    item.Name = persianMonthNames[month - 1];
                    item.Value1 = allReceipts.Count(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Enter);
                    item.Value2 = allReceipts.Count(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Exit);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
            {
                var allOrdersNotMap = await _receiptService.GetAllAsync<ReportReceiptDTO>(
                   x => !x.Deleted &&
                        x.StoreId == dto.StoreId &&
                        //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                        (dto.AccountPartyId == Guid.Empty || x.AccountPartyId == dto.AccountPartyId),
                   null,
                   null,
                   false,
                   cancellationToken);

                foreach (var receipt in allOrdersNotMap)
                {
                    var orderYear = persianCalendar.GetYear(receipt.Date);
                    if (result.Items.Any(x => x.Name == orderYear.ToString()))
                    {
                        if (receipt.Type == ReceiptType.Enter)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value1++;
                        else if (receipt.Type == ReceiptType.Exit)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value2++;
                    }
                    else
                    {
                        if (receipt.Type == ReceiptType.Enter)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value1 = 1
                            });
                        else if (receipt.Type == ReceiptType.Exit)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value2 = 1
                            });
                    }
                }
            }

            result.Total1 = result.Items.Sum(x => x.Value1);
            result.Total2 = result.Items.Sum(x => x.Value2);

            return result;
        }

        public async Task<ReportChartDTO> GetReceiptPriceChart(SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            var result = new ReportChartDTO();
            var persianCalendar = new PersianCalendar();
            DateTime today = DateTime.Today;

            // Get the current Persian year, month
            int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
            int persianMonth = persianCalendar.GetMonth(today);

            if (dto.ChartReportTiming == ReportTimeFrame.Daily)
            {
                DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
                int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

                var allReceipts = await _receiptService.GetAllAsync<ReportReceiptDTO>(
                    x => x.Date >= firstDayOfMonth &&
                         x.StoreId == dto.StoreId &&
                         //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                         (dto.AccountPartyId == Guid.Empty || x.AccountPartyId == dto.AccountPartyId),
                    i => i.Include(x => x.ProductItems).Include(x => x.ServiceItems),
                    null,
                    false,
                    cancellationToken);

                for (int day = 1; day <= lastDayInMonth; day++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

                    item.Name = $"{day: 00}";
                    item.Value1 = allReceipts.Where(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Enter).Sum(x => x.TotalFinalPrice);
                    item.Value2 = allReceipts.Where(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Exit).Sum(x => x.TotalFinalPrice);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
            {
                string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
                DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);

                var allReceipts = await _receiptService.GetAllAsync<ReportReceiptDTO>(
                     x => x.Date >= currentYear &&
                          x.StoreId == dto.StoreId &&
                          //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                          (dto.AccountPartyId == Guid.Empty || x.AccountPartyId == dto.AccountPartyId),
                     i => i.Include(x => x.ProductItems).Include(x => x.ServiceItems),
                     null,
                     false,
                     cancellationToken);

                for (int month = 1; month <= 12; month++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
                    var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
                    DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

                    item.Name = persianMonthNames[month - 1];
                    item.Value1 = allReceipts.Where(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Enter).Sum(x => x.TotalFinalPrice);
                    item.Value2 = allReceipts.Where(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Exit).Sum(x => x.TotalFinalPrice);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
            {
                var allReceipts = await _receiptService.GetAllAsync<ReportReceiptDTO>(
                   x => !x.Deleted &&
                        x.StoreId == dto.StoreId &&
                        //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                        (dto.AccountPartyId == Guid.Empty || x.AccountPartyId == dto.AccountPartyId),
                   i => i.Include(x => x.ProductItems).Include(x => x.ServiceItems),
                   null,
                   false,
                   cancellationToken);

                foreach (var receipt in allReceipts)
                {
                    var orderYear = persianCalendar.GetYear(receipt.Date);
                    if (result.Items.Any(x => x.Name == orderYear.ToString()))
                    {
                        if (receipt.Type == ReceiptType.Enter)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value1 += receipt.TotalFinalPrice;
                        else if (receipt.Type == ReceiptType.Exit)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value2 += receipt.TotalFinalPrice;
                    }
                    else
                    {
                        if (receipt.Type == ReceiptType.Enter)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value1 = receipt.TotalFinalPrice,
                            });
                        else if (receipt.Type == ReceiptType.Exit)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value2 = receipt.TotalFinalPrice,
                            });
                    }
                }
            }

            result.Total1 = result.Items.Sum(x => x.Value1);
            result.Total2 = result.Items.Sum(x => x.Value2);

            return result;
        }


        public async Task<ReportChartDTO> GetReceiptProductItemCountChart(SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            var result = new ReportChartDTO();
            var persianCalendar = new PersianCalendar();
            DateTime today = DateTime.Today;

            // Get the current Persian year, month
            int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
            int persianMonth = persianCalendar.GetMonth(today);

            if (dto.ChartReportTiming == ReportTimeFrame.Daily)
            {
                DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
                int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

                var allReceiptProductItems = await _receiptProductItemService.GetAllAsync<ReportReceiptItemDTO>(
                    x => x.Receipt.Date >= firstDayOfMonth &&
                         x.Receipt.StoreId == dto.StoreId &&
                         //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                         (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                         (dto.ProductId == Guid.Empty || x.ProductId == dto.ProductId),
                    i => i.Include(x => x.Receipt),
                    null,
                    false,
                    cancellationToken);

                for (int day = 1; day <= lastDayInMonth; day++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

                    item.Name = $"{day: 00}";
                    item.Value1 = allReceiptProductItems.Where(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Enter).Sum(x => x.Count);
                    item.Value2 = allReceiptProductItems.Where(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Exit).Sum(x => x.Count);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
            {
                string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
                DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);

                var allReceiptProductItems = await _receiptProductItemService.GetAllAsync<ReportReceiptItemDTO>(
                     x => x.Receipt.Date >= currentYear &&
                          x.Receipt.StoreId == dto.StoreId &&
                          //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                          (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                          (dto.ProductId == Guid.Empty || x.ProductId == dto.ProductId),
                     i => i.Include(x => x.Receipt),
                     null,
                     false,
                     cancellationToken);

                for (int month = 1; month <= 12; month++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
                    var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
                    DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

                    item.Name = persianMonthNames[month - 1];
                    item.Value1 = allReceiptProductItems.Where(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Enter).Sum(x => x.Count);
                    item.Value2 = allReceiptProductItems.Where(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Exit).Sum(x => x.Count);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
            {
                var allReceiptProductItems = await _receiptProductItemService.GetAllAsync<ReportReceiptItemDTO>(
                   x => !x.Deleted &&
                        x.Receipt.StoreId == dto.StoreId &&
                        //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                        (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                        (dto.ProductId == Guid.Empty || x.ProductId == dto.ProductId),
                   i => i.Include(x => x.Receipt),
                   null,
                   false,
                   cancellationToken);

                foreach (var receiptProductItem in allReceiptProductItems)
                {
                    var orderYear = persianCalendar.GetYear(receiptProductItem.Date);
                    if (result.Items.Any(x => x.Name == orderYear.ToString()))
                    {
                        if (receiptProductItem.Type == ReceiptType.Enter)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value1 += receiptProductItem.Count;
                        else if (receiptProductItem.Type == ReceiptType.Exit)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value2 += receiptProductItem.Count;
                    }
                    else
                    {
                        if (receiptProductItem.Type == ReceiptType.Enter)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value1 = receiptProductItem.Count
                            });
                        else if (receiptProductItem.Type == ReceiptType.Exit)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value2 = receiptProductItem.Count
                            });
                    }
                }
            }

            result.Total1 = result.Items.Sum(x => x.Value1);
            result.Total2 = result.Items.Sum(x => x.Value2);

            return result;
        }

        public async Task<ReportChartDTO> GetReceiptProductItemPriceChart(SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            var result = new ReportChartDTO();
            var persianCalendar = new PersianCalendar();
            DateTime today = DateTime.Today;

            // Get the current Persian year, month
            int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
            int persianMonth = persianCalendar.GetMonth(today);

            if (dto.ChartReportTiming == ReportTimeFrame.Daily)
            {
                DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
                int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

                var allReceiptProductItems = await _receiptProductItemService.GetAllAsync<ReportReceiptItemDTO>(
                    x => x.Receipt.Date >= firstDayOfMonth &&
                         x.Receipt.StoreId == dto.StoreId &&
                         //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                         (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                         (dto.ProductId == Guid.Empty || x.ProductId == dto.ProductId),
                    i => i.Include(x => x.Receipt),
                    null,
                    false,
                    cancellationToken);

                for (int day = 1; day <= lastDayInMonth; day++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

                    item.Name = $"{day: 00}";
                    item.Value1 = allReceiptProductItems.Where(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Enter).Sum(x => x.TotalFinalPrice);
                    item.Value2 = allReceiptProductItems.Where(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Exit).Sum(x => x.TotalFinalPrice);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
            {
                string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
                DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);

                var allReceiptProductItems = await _receiptProductItemService.GetAllAsync<ReportReceiptItemDTO>(
                     x => x.Receipt.Date >= currentYear &&
                          x.Receipt.StoreId == dto.StoreId &&
                          //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                          (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                          (dto.ProductId == Guid.Empty || x.ProductId == dto.ProductId),
                     i => i.Include(x => x.Receipt),
                     null,
                     false,
                     cancellationToken);

                for (int month = 1; month <= 12; month++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
                    var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
                    DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

                    item.Name = persianMonthNames[month - 1];
                    item.Value1 = allReceiptProductItems.Where(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Enter).Sum(x => x.TotalFinalPrice);
                    item.Value2 = allReceiptProductItems.Where(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Exit).Sum(x => x.TotalFinalPrice);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
            {
                var allReceiptProductItems = await _receiptProductItemService.GetAllAsync<ReportReceiptItemDTO>(
                   x => !x.Deleted &&
                        x.Receipt.StoreId == dto.StoreId &&
                        //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                        (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                        (dto.ProductId == Guid.Empty || x.ProductId == dto.ProductId),
                   i => i.Include(x => x.Receipt),
                   null,
                   false,
                   cancellationToken);

                foreach (var receiptProductItem in allReceiptProductItems)
                {
                    var orderYear = persianCalendar.GetYear(receiptProductItem.Date);
                    if (result.Items.Any(x => x.Name == orderYear.ToString()))
                    {
                        if (receiptProductItem.Type == ReceiptType.Enter)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value1 += receiptProductItem.TotalFinalPrice;
                        else if (receiptProductItem.Type == ReceiptType.Exit)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value2 += receiptProductItem.TotalFinalPrice;
                    }
                    else
                    {
                        if (receiptProductItem.Type == ReceiptType.Enter)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value1 = receiptProductItem.TotalFinalPrice
                            });
                        else if (receiptProductItem.Type == ReceiptType.Exit)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value2 = receiptProductItem.TotalFinalPrice
                            });
                    }
                }
            }

            result.Total1 = result.Items.Sum(x => x.Value1);
            result.Total2 = result.Items.Sum(x => x.Value2);

            return result;
        }


        public async Task<ReportChartDTO> GetReceiptServiceItemCountChart(SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            var result = new ReportChartDTO();
            var persianCalendar = new PersianCalendar();
            DateTime today = DateTime.Today;

            // Get the current Persian year, month
            int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
            int persianMonth = persianCalendar.GetMonth(today);

            if (dto.ChartReportTiming == ReportTimeFrame.Daily)
            {
                DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
                int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

                var allReceiptServiceItems = await _receiptServiceItemService.GetAllAsync<ReportReceiptItemDTO>(
                    x => x.Receipt.Date >= firstDayOfMonth &&
                         x.Receipt.StoreId == dto.StoreId &&
                         //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                         (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                         (dto.ServiceId == Guid.Empty || x.ServiceId == dto.ServiceId),
                    i => i.Include(x => x.Receipt),
                    null,
                    false,
                    cancellationToken);

                for (int day = 1; day <= lastDayInMonth; day++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

                    item.Name = $"{day: 00}";
                    item.Value1 = allReceiptServiceItems.Where(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Enter).Sum(x => x.Count);
                    item.Value2 = allReceiptServiceItems.Where(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Exit).Sum(x => x.Count);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
            {
                string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
                DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);

                var allReceiptServiceItems = await _receiptServiceItemService.GetAllAsync<ReportReceiptItemDTO>(
                     x => x.Receipt.Date >= currentYear &&
                          x.Receipt.StoreId == dto.StoreId &&
                          //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                          (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                          (dto.ServiceId == Guid.Empty || x.ServiceId == dto.ServiceId),
                     i => i.Include(x => x.Receipt),
                     null,
                     false,
                     cancellationToken);

                for (int month = 1; month <= 12; month++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
                    var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
                    DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

                    item.Name = persianMonthNames[month - 1];
                    item.Value1 = allReceiptServiceItems.Where(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Enter).Sum(x => x.Count);
                    item.Value2 = allReceiptServiceItems.Where(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Exit).Sum(x => x.Count);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
            {
                var allReceiptServiceItems = await _receiptServiceItemService.GetAllAsync<ReportReceiptItemDTO>(
                   x => !x.Deleted &&
                        x.Receipt.StoreId == dto.StoreId &&
                        //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                        (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                        (dto.ServiceId == Guid.Empty || x.ServiceId == dto.ServiceId),
                   i => i.Include(x => x.Receipt),
                   null,
                   false,
                   cancellationToken);

                foreach (var receiptServiceItem in allReceiptServiceItems)
                {
                    var orderYear = persianCalendar.GetYear(receiptServiceItem.Date);
                    if (result.Items.Any(x => x.Name == orderYear.ToString()))
                    {
                        if (receiptServiceItem.Type == ReceiptType.Enter)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value1 += receiptServiceItem.Count;
                        else if (receiptServiceItem.Type == ReceiptType.Exit)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value2 += receiptServiceItem.Count;
                    }
                    else
                    {
                        if (receiptServiceItem.Type == ReceiptType.Enter)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value1 = receiptServiceItem.Count
                            });
                        else if (receiptServiceItem.Type == ReceiptType.Exit)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value2 = receiptServiceItem.Count
                            });
                    }
                }
            }

            result.Total1 = result.Items.Sum(x => x.Value1);
            result.Total2 = result.Items.Sum(x => x.Value2);

            return result;
        }

        public async Task<ReportChartDTO> GetReceiptServiceItemPriceChart(SearchReportChartDTO dto, CancellationToken cancellationToken)
        {
            var result = new ReportChartDTO();
            var persianCalendar = new PersianCalendar();
            DateTime today = DateTime.Today;

            // Get the current Persian year, month
            int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
            int persianMonth = persianCalendar.GetMonth(today);

            if (dto.ChartReportTiming == ReportTimeFrame.Daily)
            {
                DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
                int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

                var allReceiptServiceItems = await _receiptServiceItemService.GetAllAsync<ReportReceiptItemDTO>(
                    x => x.Receipt.Date >= firstDayOfMonth &&
                         x.Receipt.StoreId == dto.StoreId &&
                         //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                         (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                         (dto.ServiceId == Guid.Empty || x.ServiceId == dto.ServiceId),
                    i => i.Include(x => x.Receipt),
                    null,
                    false,
                    cancellationToken);

                for (int day = 1; day <= lastDayInMonth; day++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

                    item.Name = $"{day: 00}";
                    item.Value1 = allReceiptServiceItems.Where(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Enter).Sum(x => x.TotalFinalPrice);
                    item.Value2 = allReceiptServiceItems.Where(x => x.Date.Date == currentDay.Date && x.Type == ReceiptType.Exit).Sum(x => x.TotalFinalPrice);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
            {
                string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
                DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);

                var allReceiptServiceItems = await _receiptServiceItemService.GetAllAsync<ReportReceiptItemDTO>(
                     x => x.Receipt.Date >= currentYear &&
                          x.Receipt.StoreId == dto.StoreId &&
                          //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                          (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                          (dto.ServiceId == Guid.Empty || x.ServiceId == dto.ServiceId),
                     i => i.Include(x => x.Receipt),
                     null,
                     false,
                     cancellationToken);

                for (int month = 1; month <= 12; month++)
                {
                    var item = new ReportChartItemDTO();
                    DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
                    var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
                    DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

                    item.Name = persianMonthNames[month - 1];
                    item.Value1 = allReceiptServiceItems.Where(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Enter).Sum(x => x.TotalFinalPrice);
                    item.Value2 = allReceiptServiceItems.Where(x => x.Date >= currentMonthStartDay && x.Date <= currentMonthEndDay && x.Type == ReceiptType.Exit).Sum(x => x.TotalFinalPrice);

                    result.Items.Add(item);
                }
            }
            else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
            {
                var allReceiptServiceItems = await _receiptServiceItemService.GetAllAsync<ReportReceiptItemDTO>(
                   x => !x.Deleted &&
                        x.Receipt.StoreId == dto.StoreId &&
                        //(dto.VendorId == Guid.Empty || x.VendorId == dto.VendorId) &&
                        (dto.AccountPartyId == Guid.Empty || x.Receipt.AccountPartyId == dto.AccountPartyId) &&
                        (dto.ServiceId == Guid.Empty || x.ServiceId == dto.ServiceId),
                   i => i.Include(x => x.Receipt),
                   null,
                   false,
                   cancellationToken);

                foreach (var receiptServiceItem in allReceiptServiceItems)
                {
                    var orderYear = persianCalendar.GetYear(receiptServiceItem.Date);
                    if (result.Items.Any(x => x.Name == orderYear.ToString()))
                    {
                        if (receiptServiceItem.Type == ReceiptType.Enter)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value1 += receiptServiceItem.TotalFinalPrice;
                        else if (receiptServiceItem.Type == ReceiptType.Exit)
                            result.Items.First(x => x.Name == orderYear.ToString()).Value2 += receiptServiceItem.TotalFinalPrice;
                    }
                    else
                    {
                        if (receiptServiceItem.Type == ReceiptType.Enter)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value1 = receiptServiceItem.TotalFinalPrice
                            });
                        else if (receiptServiceItem.Type == ReceiptType.Exit)
                            result.Items.Add(new ReportChartItemDTO()
                            {
                                Name = orderYear.ToString(),
                                Value2 = receiptServiceItem.TotalFinalPrice
                            });
                    }
                }
            }

            result.Total1 = result.Items.Sum(x => x.Value1);
            result.Total2 = result.Items.Sum(x => x.Value2);

            return result;
        }



        //public async Task<ReportChartDTO> GetPlateAreaChart(SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    var result = new ReportChartDTO();
        //    var persianCalendar = new PersianCalendar();
        //    DateTime today = DateTime.Today;

        //    // Get the current Persian year, month
        //    int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
        //    int persianMonth = persianCalendar.GetMonth(today);

        //    if (dto.ChartReportTiming == ReportTimeFrame.Daily)
        //    {
        //        DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
        //        int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

        //        var allPlates = await _plateService.GetAllAsync<PlateDateDTO>(
        //            x => x.CreateDate >= firstDayOfMonth &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.KlischeTypeId == dto.KlischeTypeId),
        //            null,
        //            null,
        //            false,
        //            cancellationToken);

        //        for (int day = 1; day <= lastDayInMonth; day++)
        //        {
        //            var item = new ReportChartItemDTO();
        //            DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

        //            item.Name = day.ToString();
        //            item.Value = (decimal)allPlates.Where(x => x.CreateDate.Date == currentDay.Date).Sum(x => x.Area);
        //            item.Value1 = (decimal)allPlates.Where(x => x.CreateDate.Date == currentDay.Date && x.EdgeWasteArea.HasValue).Sum(x => x.EdgeWasteArea);

        //            result.Items.Add(item);
        //        }
        //    }
        //    else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
        //    {
        //        string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        //        DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
        //        var allPlates = await _plateService.GetAllAsync<PlateDateDTO>(
        //           x => x.CreateDate.Date >= currentYear.Date &&
        //                (dto.KlischeThicknessId == Guid.Empty || x.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                (dto.KlischeTypeId == Guid.Empty || x.KlischeTypeId == dto.KlischeTypeId),
        //           null,
        //           null,
        //           false,
        //           cancellationToken);

        //        for (int month = 1; month <= 12; month++)
        //        {
        //            var item = new ReportChartItemDTO();
        //            DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
        //            var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
        //            DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

        //            item.Name = persianMonthNames[month - 1];
        //            item.Value = (decimal)allPlates.Where(x => x.CreateDate >= currentMonthStartDay && x.CreateDate <= currentMonthEndDay).Sum(x => x.Area);
        //            item.Value1 = (decimal)allPlates.Where(x => x.CreateDate >= currentMonthStartDay && x.CreateDate <= currentMonthEndDay && x.EdgeWasteArea.HasValue).Sum(x => x.EdgeWasteArea);
        //            result.Items.Add(item);
        //        }
        //    }
        //    else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
        //    {
        //        var allPlates = await _plateService.GetAllAsync<PlateDateDTO>(
        //            x => !x.Deleted &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.KlischeTypeId == dto.KlischeTypeId),
        //            null,
        //            null,
        //            false,
        //            cancellationToken);

        //        foreach (var plate in allPlates)
        //        {
        //            var orderYear = persianCalendar.GetYear(plate.CreateDate);
        //            if (result.Items.Any(x => x.Name == orderYear.ToString()))
        //            {
        //                result.Items.First(x => x.Name == orderYear.ToString()).Value += (decimal)plate.Area;
        //                result.Items.First(x => x.Name == orderYear.ToString()).Value1 += plate.EdgeWasteArea.HasValue ? (decimal)plate.EdgeWasteArea : 0;
        //            }
        //            else
        //                result.Items.Add(new ReportChartItemDTO()
        //                {
        //                    Name = orderYear.ToString(),
        //                    Value = (decimal)plate.Area,
        //                    Value1 = (decimal)plate.EdgeWasteArea
        //                });
        //        }
        //    }
        //    result.Total = result.Items.Sum(x => x.Value);
        //    result.Total1 = result.Items.Sum(x => x.Value1);

        //    return result;
        //}

        //public async Task<ReportChartDTO> GetPlateDamageAreaChart(SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    var result = new ReportChartDTO();
        //    var persianCalendar = new PersianCalendar();
        //    DateTime today = DateTime.Today;

        //    // Get the current Persian year, month
        //    int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
        //    int persianMonth = persianCalendar.GetMonth(today);

        //    if (dto.ChartReportTiming == ReportTimeFrame.Daily)
        //    {
        //        DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
        //        int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

        //        var allColorDamages = await _damageColorService.GetAllAsync<ColorDamageDateDTO>(
        //            x => x.CreateDate >= firstDayOfMonth &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Damage.Plate.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Damage.Plate.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.Damage).ThenInclude(x => x.Plate),
        //            null,
        //            false,
        //            cancellationToken);

        //        for (int day = 1; day <= lastDayInMonth; day++)
        //        {
        //            var item = new ReportChartItemDTO();
        //            DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

        //            item.Name = day.ToString();
        //            item.Value = (decimal)allColorDamages.Where(x => x.CreateDate.Date == currentDay.Date).Sum(x => x.Area);

        //            result.Items.Add(item);
        //        }
        //    }
        //    else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
        //    {
        //        string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        //        DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
        //        var allColorDamages = await _damageColorService.GetAllAsync<ColorDamageDateDTO>(
        //           x => x.CreateDate.Date >= currentYear.Date &&
        //                (dto.KlischeThicknessId == Guid.Empty || x.Damage.Plate.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                (dto.KlischeTypeId == Guid.Empty || x.Damage.Plate.KlischeTypeId == dto.KlischeTypeId),
        //           i => i.Include(x => x.Damage).ThenInclude(x => x.Plate),
        //           null,
        //           false,
        //           cancellationToken);

        //        for (int month = 1; month <= 12; month++)
        //        {
        //            var item = new ReportChartItemDTO();
        //            DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
        //            var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
        //            DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

        //            item.Name = persianMonthNames[month - 1];
        //            item.Value = (decimal)allColorDamages.Where(x => x.CreateDate >= currentMonthStartDay && x.CreateDate <= currentMonthEndDay).Sum(x => x.Area);

        //            result.Items.Add(item);
        //        }
        //    }
        //    else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
        //    {
        //        var allColorDamages = await _damageColorService.GetAllAsync<ColorDamageDateDTO>(
        //            x => !x.Deleted &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Damage.Plate.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Damage.Plate.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.Damage).ThenInclude(x => x.Plate),
        //            null,
        //            false,
        //            cancellationToken);

        //        foreach (var colorDamage in allColorDamages)
        //        {
        //            var orderYear = persianCalendar.GetYear(colorDamage.CreateDate);
        //            if (result.Items.Any(x => x.Name == orderYear.ToString()))
        //            {
        //                result.Items.First(x => x.Name == orderYear.ToString()).Value += (decimal)colorDamage.Area;
        //            }
        //            else
        //                result.Items.Add(new ReportChartItemDTO()
        //                {
        //                    Name = orderYear.ToString(),
        //                    Value = (decimal)colorDamage.Area
        //                });
        //        }
        //    }

        //    result.Total = result.Items.Sum(x => x.Value);
        //    return result;
        //}

        //public async Task<ReportChartDTO> GetDesignerOrderRegisterCountChart(ReportTimeFrame chartReportTiming, CancellationToken cancellationToken)
        //{
        //    DateTime now = DateTime.Now;
        //    DateTime startDate = new DateTime();
        //    DateTime endDate = new DateTime();

        //    var persianCalendar = new PersianCalendar();
        //    int persianYear = persianCalendar.GetYear(now);
        //    int persianMonth = persianCalendar.GetMonth(now);

        //    switch (chartReportTiming)
        //    {
        //        case ReportTimeFrame.Daily:
        //            int dayOfWeek = (int)now.DayOfWeek;
        //            DateTime startOfWeek = now.AddDays(dayOfWeek == 6 ? 0 : -(dayOfWeek + 1)).Date;
        //            startDate = new DateTime(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day, 0, 0, 0);
        //            DateTime endOfWeek = startOfWeek.AddDays(6);
        //            endDate = new DateTime(endOfWeek.Year, endOfWeek.Month, endOfWeek.Day, 23, 59, 59);
        //            break;
        //        case ReportTimeFrame.Monthly:
        //            startDate = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
        //            int daysInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);
        //            endDate = persianCalendar.ToDateTime(persianYear, persianMonth, daysInMonth, 23, 59, 59, 0);
        //            break;
        //        case ReportTimeFrame.Yearly:
        //            bool isKabise = persianCalendar.IsLeapYear(persianYear);
        //            startDate = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
        //            endDate = persianCalendar.ToDateTime(persianYear, 12, isKabise ? 30 : 29, 23, 59, 59, 0);
        //            break;
        //    }

        //    var result = new ReportChartDTO();
        //    result.Items = await _userService.GetAllAsync<ReportChartItemDTO>(
        //        x => x.Orders.Any(z => z.CreateDate >= startDate && z.CreateDate <= endDate),
        //        i => i.Include(x => x.Orders.Where(z => z.CreateDate >= startDate && z.CreateDate <= endDate))
        //              .Include(x => x.Person),
        //        o => o.OrderByDescending(x => x.Orders.Where(z => z.CreateDate >= startDate && z.CreateDate <= endDate).Count()),
        //        false,
        //        cancellationToken);


        //    result.Total = result.Items.Sum(x => x.Value);
        //    result.Total1 = result.Items.Sum(x => x.Value1);
        //    result.Total2 = result.Items.Sum(x => x.Value2);
        //    result.Total3 = result.Items.Sum(x => x.Value3);
        //    result.Items = result.Items.Take(20).ToList();

        //    return result;
        //}

        //public async Task<ReportChartDTO> GetDesignerOrderRegisterAreaChart(ReportTimeFrame chartReportTiming, CancellationToken cancellationToken)
        //{
        //    DateTime now = DateTime.Now;
        //    DateTime startDate = new DateTime();
        //    DateTime endDate = new DateTime();

        //    var persianCalendar = new PersianCalendar();
        //    int persianYear = persianCalendar.GetYear(now);
        //    int persianMonth = persianCalendar.GetMonth(now);

        //    switch (chartReportTiming)
        //    {
        //        case ReportTimeFrame.Daily:
        //            int dayOfWeek = (int)now.DayOfWeek;
        //            DateTime startOfWeek = now.AddDays(dayOfWeek == 6 ? 0 : -(dayOfWeek + 1)).Date;
        //            startDate = new DateTime(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day, 0, 0, 0);
        //            DateTime endOfWeek = startOfWeek.AddDays(6);
        //            endDate = new DateTime(endOfWeek.Year, endOfWeek.Month, endOfWeek.Day, 23, 59, 59);
        //            break;
        //        case ReportTimeFrame.Monthly:
        //            startDate = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
        //            int daysInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);
        //            endDate = persianCalendar.ToDateTime(persianYear, persianMonth, daysInMonth, 23, 59, 59, 0);
        //            break;
        //        case ReportTimeFrame.Yearly:
        //            bool isKabise = persianCalendar.IsLeapYear(persianYear);
        //            startDate = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
        //            endDate = persianCalendar.ToDateTime(persianYear, 12, isKabise ? 30 : 29, 23, 59, 59, 0);
        //            break;
        //    }

        //    var result = new ReportChartDTO();
        //    result.Items2 = await _userService.GetAllAsync<ReportChartItem2DTO>(
        //        x => x.Orders.Any(z => z.CreateDate >= startDate && z.CreateDate <= endDate),
        //        i => i.Include(x => x.Orders.Where(z => z.CreateDate >= startDate && z.CreateDate <= endDate)).ThenInclude(x => x.RipColorAngles)
        //              .Include(x => x.Person),
        //        null,
        //        false,
        //        cancellationToken);

        //    result.Total = result.Items2.Sum(x => x.Value);
        //    result.Total1 = result.Items2.Sum(x => x.Value1);
        //    result.Total2 = result.Items2.Sum(x => x.Value2);
        //    result.Total3 = result.Items2.Sum(x => x.Value3);
        //    result.Items2 = result.Items2.OrderByDescending(x => x.Value + x.Value1 + x.Value2 + x.Value3).Take(20).ToList();

        //    return result;
        //}

        //public async Task<ReportChartDTO> GetAvrageKlischePrice(SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    var result = new ReportChartDTO();
        //    var persianCalendar = new PersianCalendar();
        //    DateTime today = DateTime.Today;

        //    // Get the current Persian year, month
        //    int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
        //    int persianMonth = persianCalendar.GetMonth(today);

        //    if (dto.ChartReportTiming == ReportTimeFrame.Daily)
        //    {
        //        DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
        //        int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

        //        var allInvoices = await _invoiceService.GetAllAsync<InvoiceDateDTO>(
        //            x => x.InvoiceStatus == Entities.Enums.Rasam.InvoiceStatus.Active &&
        //                 x.CreateDate >= firstDayOfMonth &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Order.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Order.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.InvoiceItems)
        //                .Include(x => x.Order).ThenInclude(x => x.RipColorAngles),
        //            null,
        //            false,
        //            cancellationToken);

        //        var totalArea = allInvoices.Sum(x => x.Area);
        //        for (int day = 1; day <= lastDayInMonth; day++)
        //        {
        //            var item = new ReportChartItemDTO();
        //            DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

        //            item.Name = day.ToString();
        //            var klischePrice = allInvoices.Where(x => x.CreateDate.Date == currentDay.Date).Sum(x => x.Area * x.UnitPrice);
        //            item.Value = (decimal)(klischePrice / totalArea);
        //            result.Items.Add(item);
        //        }
        //    }
        //    else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
        //    {
        //        string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        //        DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
        //        var allInvoices = await _invoiceService.GetAllAsync<InvoiceDateDTO>(
        //            x => x.InvoiceStatus == Entities.Enums.Rasam.InvoiceStatus.Active &&
        //                 x.CreateDate.Date >= currentYear.Date &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Order.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Order.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.InvoiceItems)
        //                  .Include(x => x.Order).ThenInclude(x => x.RipColorAngles),
        //            null,
        //            false,
        //            cancellationToken);

        //        for (int month = 1; month <= 12; month++)
        //        {
        //            var item = new ReportChartItemDTO();
        //            DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
        //            var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
        //            DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

        //            item.Name = persianMonthNames[month - 1];
        //            var totalArea = allInvoices.Where(x => x.CreateDate >= currentMonthStartDay && x.CreateDate <= currentMonthEndDay).Sum(x => x.Area);
        //            var klischePrice = allInvoices.Where(x => x.CreateDate >= currentMonthStartDay && x.CreateDate <= currentMonthEndDay).Sum(x => x.Area * x.UnitPrice);
        //            item.Value = totalArea == 0 ? 0 : (decimal)(klischePrice / totalArea);
        //            result.Items.Add(item);
        //        }
        //    }
        //    else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
        //    {
        //        var allInvoices = await _invoiceService.GetAllAsync<InvoiceDateDTO>(
        //            x => x.InvoiceStatus == Entities.Enums.Rasam.InvoiceStatus.Active &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Order.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Order.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.InvoiceItems)
        //                 .Include(x => x.Order).ThenInclude(x => x.RipColorAngles),
        //            null,
        //            false,
        //            cancellationToken);

        //        foreach (var invoice in allInvoices)
        //        {
        //            var totalArea = allInvoices.Sum(x => x.Area);
        //            var invoiceYear = persianCalendar.GetYear(invoice.CreateDate);
        //            invoice.Year = invoiceYear;
        //        }

        //        var grouped = allInvoices.GroupBy(x => x.Year).Select(x => new { Year = x.Key, totalArea = x.Sum(z => z.Area), klischePrice = x.Sum(z => z.Area * z.UnitPrice) });
        //        foreach (var year in grouped)
        //        {
        //            var item = new ReportChartItemDTO();
        //            item.Name = year.Year.ToString();
        //            item.Value = year.totalArea == 0 ? 0 : (decimal)(year.klischePrice / year.totalArea);
        //            result.Items.Add(item);
        //        }
        //    }

        //    return result;
        //}

        //public async Task<ReportChartDTO> GetAvrageKlischeArea(SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    var result = new ReportChartDTO();
        //    var persianCalendar = new PersianCalendar();
        //    DateTime today = DateTime.Today;

        //    // Get the current Persian year, month
        //    int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
        //    int persianMonth = persianCalendar.GetMonth(today);

        //    if (dto.ChartReportTiming == ReportTimeFrame.Daily)
        //    {
        //        DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
        //        int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

        //        var allInvoices = await _invoiceService.GetAllAsync<InvoiceDateDTO>(
        //            x => x.InvoiceStatus == Entities.Enums.Rasam.InvoiceStatus.Active &&
        //                 x.CreateDate >= firstDayOfMonth &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Order.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Order.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.InvoiceItems)
        //                .Include(x => x.Order).ThenInclude(x => x.RipColorAngles),
        //            null,
        //            false,
        //            cancellationToken);

        //        for (int day = 1; day <= lastDayInMonth; day++)
        //        {
        //            var item = new ReportChartItemDTO();
        //            DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

        //            item.Name = day.ToString();
        //            var area = allInvoices.Where(x => x.CreateDate.Date == currentDay.Date).Sum(x => x.Area);
        //            item.Value = (decimal)area;
        //            result.Items.Add(item);
        //        }
        //    }
        //    else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
        //    {
        //        string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        //        DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
        //        var allInvoices = await _invoiceService.GetAllAsync<InvoiceDateDTO>(
        //            x => x.InvoiceStatus == Entities.Enums.Rasam.InvoiceStatus.Active &&
        //                 x.CreateDate.Date >= currentYear.Date &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Order.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Order.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.InvoiceItems)
        //                  .Include(x => x.Order).ThenInclude(x => x.RipColorAngles),
        //            null,
        //            false,
        //            cancellationToken);

        //        for (int month = 1; month <= 12; month++)
        //        {
        //            var item = new ReportChartItemDTO();
        //            DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
        //            var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
        //            DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

        //            item.Name = persianMonthNames[month - 1];
        //            var totalArea = allInvoices.Where(x => x.CreateDate >= currentMonthStartDay && x.CreateDate <= currentMonthEndDay).Sum(x => x.Area);
        //            item.Value = (decimal)totalArea;
        //            result.Items.Add(item);
        //        }
        //    }
        //    else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
        //    {
        //        var allInvoices = await _invoiceService.GetAllAsync<InvoiceDateDTO>(
        //            x => x.InvoiceStatus == Entities.Enums.Rasam.InvoiceStatus.Active &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Order.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Order.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.InvoiceItems)
        //                 .Include(x => x.Order).ThenInclude(x => x.RipColorAngles),
        //            null,
        //            false,
        //            cancellationToken);

        //        foreach (var invoice in allInvoices)
        //        {
        //            var totalArea = allInvoices.Sum(x => x.Area);
        //            var invoiceYear = persianCalendar.GetYear(invoice.CreateDate);
        //            invoice.Year = invoiceYear;
        //        }

        //        var grouped = allInvoices.GroupBy(x => x.Year).Select(x => new { Year = x.Key, totalArea = x.Sum(z => z.Area) });
        //        foreach (var year in grouped)
        //        {
        //            var item = new ReportChartItemDTO();
        //            item.Name = year.Year.ToString();
        //            item.Value = (decimal)year.totalArea;
        //            result.Items.Add(item);
        //        }
        //    }

        //    return result;
        //}

        //public async Task<ReportChartDTO> GetKlischePrice(SearchReportChartDTO dto, CancellationToken cancellationToken)
        //{
        //    var result = new ReportChartDTO();
        //    var persianCalendar = new PersianCalendar();
        //    DateTime today = DateTime.Today;

        //    // Get the current Persian year, month
        //    int persianYear = dto.Year == 0 ? persianCalendar.GetYear(today) : dto.Year;
        //    int persianMonth = persianCalendar.GetMonth(today);

        //    if (dto.ChartReportTiming == ReportTimeFrame.Daily)
        //    {
        //        DateTime firstDayOfMonth = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
        //        int lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);

        //        var allInvoices = await _invoiceService.GetAllAsync<InvoiceDateDTO>(
        //            x => x.InvoiceStatus == Entities.Enums.Rasam.InvoiceStatus.Active &&
        //                 x.CreateDate >= firstDayOfMonth &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Order.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Order.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.InvoiceItems)
        //                .Include(x => x.Order).ThenInclude(x => x.RipColorAngles),
        //            null,
        //            false,
        //            cancellationToken);

        //        for (int day = 1; day <= lastDayInMonth; day++)
        //        {
        //            var item = new ReportChartItemDTO();
        //            DateTime currentDay = persianCalendar.ToDateTime(persianYear, persianMonth, day, 0, 0, 0, 0);

        //            item.Name = day.ToString();
        //            var klischePrice = allInvoices.Where(x => x.CreateDate.Date == currentDay.Date).Sum(x => x.Area * x.UnitPrice);
        //            item.Value = (decimal)klischePrice;
        //            result.Items.Add(item);
        //        }
        //    }
        //    else if (dto.ChartReportTiming == ReportTimeFrame.Monthly)
        //    {
        //        string[] persianMonthNames = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        //        DateTime currentYear = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
        //        var allInvoices = await _invoiceService.GetAllAsync<InvoiceDateDTO>(
        //            x => x.InvoiceStatus == Entities.Enums.Rasam.InvoiceStatus.Active &&
        //                 x.CreateDate.Date >= currentYear.Date &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Order.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Order.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.InvoiceItems)
        //                  .Include(x => x.Order).ThenInclude(x => x.RipColorAngles),
        //            null,
        //            false,
        //            cancellationToken);

        //        for (int month = 1; month <= 12; month++)
        //        {
        //            var item = new ReportChartItemDTO();
        //            DateTime currentMonthStartDay = persianCalendar.ToDateTime(persianYear, month, 1, 0, 0, 0, 0);
        //            var lastDayInMonth = persianCalendar.GetDaysInMonth(persianYear, month);
        //            DateTime currentMonthEndDay = persianCalendar.ToDateTime(persianYear, month, lastDayInMonth, 23, 59, 59, 0);

        //            item.Name = persianMonthNames[month - 1];
        //            var klischePrice = allInvoices.Where(x => x.CreateDate >= currentMonthStartDay && x.CreateDate <= currentMonthEndDay).Sum(x => x.Area * x.UnitPrice);
        //            item.Value = (decimal)klischePrice;
        //            result.Items.Add(item);
        //        }
        //    }
        //    else if (dto.ChartReportTiming == ReportTimeFrame.Yearly)
        //    {
        //        var allInvoices = await _invoiceService.GetAllAsync<InvoiceDateDTO>(
        //            x => x.InvoiceStatus == Entities.Enums.Rasam.InvoiceStatus.Active &&
        //                 (dto.KlischeThicknessId == Guid.Empty || x.Order.KlischeThicknessId == dto.KlischeThicknessId) &&
        //                 (dto.KlischeTypeId == Guid.Empty || x.Order.KlischeTypeId == dto.KlischeTypeId),
        //            i => i.Include(x => x.InvoiceItems)
        //                 .Include(x => x.Order).ThenInclude(x => x.RipColorAngles),
        //            null,
        //            false,
        //            cancellationToken);

        //        foreach (var invoice in allInvoices)
        //        {
        //            var totalArea = allInvoices.Sum(x => x.Area);
        //            var invoiceYear = persianCalendar.GetYear(invoice.CreateDate);
        //            invoice.Year = invoiceYear;
        //        }

        //        var grouped = allInvoices.GroupBy(x => x.Year).Select(x => new { Year = x.Key, klischePrice = x.Sum(z => z.Area * z.UnitPrice) });
        //        foreach (var year in grouped)
        //        {
        //            var item = new ReportChartItemDTO();
        //            item.Name = year.Year.ToString();
        //            item.Value = (decimal)year.klischePrice;
        //            result.Items.Add(item);
        //        }
        //    }

        //    return result;
        //}

        //public async Task<ReportChartDTO> GetCustomerOrderRegisterCountChart(ReportTimeFrame chartReportTiming, CancellationToken cancellationToken)
        //{
        //    DateTime now = DateTime.Now;
        //    DateTime startDate = new DateTime();
        //    DateTime endDate = new DateTime();

        //    var persianCalendar = new PersianCalendar();
        //    int persianYear = persianCalendar.GetYear(now);
        //    int persianMonth = persianCalendar.GetMonth(now);

        //    switch (chartReportTiming)
        //    {
        //        case ReportTimeFrame.Daily:
        //            int dayOfWeek = (int)now.DayOfWeek;
        //            DateTime startOfWeek = now.AddDays(dayOfWeek == 6 ? 0 : -(dayOfWeek + 1)).Date;
        //            startDate = new DateTime(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day, 0, 0, 0);
        //            DateTime endOfWeek = startOfWeek.AddDays(6);
        //            endDate = new DateTime(endOfWeek.Year, endOfWeek.Month, endOfWeek.Day, 23, 59, 59);
        //            break;
        //        case ReportTimeFrame.Monthly:
        //            startDate = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
        //            int daysInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);
        //            endDate = persianCalendar.ToDateTime(persianYear, persianMonth, daysInMonth, 23, 59, 59, 0);
        //            break;
        //        case ReportTimeFrame.Yearly:
        //            bool isKabise = persianCalendar.IsLeapYear(persianYear);
        //            startDate = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
        //            endDate = persianCalendar.ToDateTime(persianYear, 12, isKabise ? 30 : 29, 23, 59, 59, 0);
        //            break;
        //    }

        //    var result = new ReportChartDTO();
        //    result.Items = await _customerService.GetAllAsync<ReportChartItemDTO>(
        //        x => x.Orders.Any(z => z.CreateDate >= startDate && z.CreateDate <= endDate),
        //        i => i.Include(x => x.Orders.Where(z => z.CreateDate >= startDate && z.CreateDate <= endDate)),
        //        o => o.OrderByDescending(x => x.Orders.Where(z => z.CreateDate >= startDate && z.CreateDate <= endDate).Count()),
        //        false,
        //        cancellationToken);


        //    result.Total = result.Items.Sum(x => x.Value);
        //    result.Total1 = result.Items.Sum(x => x.Value1);
        //    result.Total2 = result.Items.Sum(x => x.Value2);
        //    result.Total3 = result.Items.Sum(x => x.Value3);
        //    result.Items = result.Items.Take(20).ToList();

        //    return result;
        //}

        //public async Task<ReportChartDTO> GetCustomerOrderRegisterAreaChart(ReportTimeFrame chartReportTiming, CancellationToken cancellationToken)
        //{
        //    DateTime now = DateTime.Now;
        //    DateTime startDate = new DateTime();
        //    DateTime endDate = new DateTime();

        //    var persianCalendar = new PersianCalendar();
        //    int persianYear = persianCalendar.GetYear(now);
        //    int persianMonth = persianCalendar.GetMonth(now);

        //    switch (chartReportTiming)
        //    {
        //        case ReportTimeFrame.Daily:
        //            int dayOfWeek = (int)now.DayOfWeek;
        //            DateTime startOfWeek = now.AddDays(dayOfWeek == 6 ? 0 : -(dayOfWeek + 1)).Date;
        //            startDate = new DateTime(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day, 0, 0, 0);
        //            DateTime endOfWeek = startOfWeek.AddDays(6);
        //            endDate = new DateTime(endOfWeek.Year, endOfWeek.Month, endOfWeek.Day, 23, 59, 59);
        //            break;
        //        case ReportTimeFrame.Monthly:
        //            startDate = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0);
        //            int daysInMonth = persianCalendar.GetDaysInMonth(persianYear, persianMonth);
        //            endDate = persianCalendar.ToDateTime(persianYear, persianMonth, daysInMonth, 23, 59, 59, 0);
        //            break;
        //        case ReportTimeFrame.Yearly:
        //            bool isKabise = persianCalendar.IsLeapYear(persianYear);
        //            startDate = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
        //            endDate = persianCalendar.ToDateTime(persianYear, 12, isKabise ? 30 : 29, 23, 59, 59, 0);
        //            break;
        //    }

        //    var result = new ReportChartDTO();
        //    result.Items2 = await _customerService.GetAllAsync<ReportChartItem2DTO>(
        //        x => x.Orders.Any(z => z.CreateDate >= startDate && z.CreateDate <= endDate),
        //        i => i.Include(x => x.Orders.Where(z => z.CreateDate >= startDate && z.CreateDate <= endDate)).ThenInclude(x => x.RipColorAngles),
        //        null,
        //        false,
        //        cancellationToken);

        //    result.Total = result.Items2.Sum(x => x.Value);
        //    result.Total1 = result.Items2.Sum(x => x.Value1);
        //    result.Total2 = result.Items2.Sum(x => x.Value2);
        //    result.Total3 = result.Items2.Sum(x => x.Value3);
        //    result.Items2 = result.Items2.OrderByDescending(x => x.Value + x.Value1 + x.Value2 + x.Value3).Take(20).ToList();

        //    return result;
        //}

        //public async Task<ReportChartDTO> RatioInvoiceType(CancellationToken cancellationToken)
        //{
        //    var result = new ReportChartDTO();
        //    var allOrdersNotMap = await _orderRepository.GetAllAsync(x => !x.Deleted, null, null, false, cancellationToken);
        //    var allOrders = _mapper.Map<List<OrderInvoiceTypeDTO>>(allOrdersNotMap);
        //    result.Items = allOrders.GroupBy(x => x.InvoiceType).Select(x => new ReportChartItemDTO()
        //    {
        //        Name = x.Key,
        //        Value = x.Count()
        //    })
        //    .OrderByDescending(x => x.Value).ToList();

        //    result.Total = result.Items.Sum(x => x.Value);
        //    return result;
        //}

        //public async Task<ReportChartDTO> RatioOrderType(CancellationToken cancellationToken)
        //{
        //    var result = new ReportChartDTO();
        //    var allOrdersNotMap = await _orderRepository.GetAllAsync(x => !x.Deleted, null, null, false, cancellationToken);
        //    var allOrders = _mapper.Map<List<OrderTypeDTO>>(allOrdersNotMap);
        //    result.Items = allOrders.GroupBy(x => x.OrderType).Select(x => new ReportChartItemDTO()
        //    {
        //        Name = x.Key,
        //        Value = x.Count()
        //    })
        //    .OrderByDescending(x => x.Value).ToList();

        //    result.Total = result.Items.Sum(x => x.Value);
        //    return result;
        //}

        public List<YearDropdownDTO> GetYearsDropdown()
        {
            var result = new List<YearDropdownDTO>();
            var now = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            var currentYear = persianCalendar.GetYear(now);

            for (int i = 0; i > -6; i--)
            {
                result.Add(new YearDropdownDTO()
                {
                    Key = (currentYear + i).ToString(),
                    Value = currentYear + i
                });
            }

            return result;
        }
    }
}
