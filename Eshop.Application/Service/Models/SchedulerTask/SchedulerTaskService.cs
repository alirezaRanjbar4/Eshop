using AutoMapper;
using Eshop.Application.DTO.Models.SchedulerTask;
using Eshop.Application.Service.General;
using Eshop.Application.Service.Identity.User;
using Eshop.Application.Service.Models.Vendor;
using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;
using Eshop.Share.Enum;
using Eshop.Share.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Eshop.Application.Service.Models.SchedulerTask
{
    public class SchedulerTaskService : BaseService<SchedulerTaskEntity>, ISchedulerTaskService
    {
        private readonly ISchedulerTaskVendorService _schedulerTaskVendorService;
        private readonly IVendorService _personService;
        private readonly IUserService _userService;

        public SchedulerTaskService(
            IMapper mapper,
            IBaseRepository<SchedulerTaskEntity> schedulerTaskRepository,
            ISchedulerTaskVendorService schedulerTaskVendorService,
            IVendorService personService,
            IUserService userService) : base(schedulerTaskRepository, mapper)
        {
            _schedulerTaskVendorService = schedulerTaskVendorService;
            _personService = personService;
            _userService = userService;
        }


        public async Task<List<CalenderSchedulerVM>> GetSchedulerTask([FromBody] SearchSchedulerTaskDTO searchDto, CancellationToken cancellationToken)
        {
            var result = new List<CalenderSchedulerVM>();
            PersianCalendar persianCalendar = new PersianCalendar();
            var splitDate = searchDto.Date.Split("/");

            DateTime firstDayDateTime = new DateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), 1, persianCalendar);
            DateOnly firstDayDateOnly = DateOnly.FromDateTime(firstDayDateTime);
            int firstDay = (int)firstDayDateTime.DayOfWeek;
            int daysInMonth = persianCalendar.GetDaysInMonth(int.Parse(splitDate[0]), int.Parse(splitDate[1]));
            int skip = 0;
            switch (firstDay)
            {
                case 0:
                    skip = 1;
                    break;
                case 1:
                    skip = 2;
                    break;
                case 2:
                    skip = 3;
                    break;
                case 3:
                    skip = 4;
                    break;
                case 4:
                    skip = 5;
                    break;
                case 5:
                    skip = 6;
                    break;
                case 6:
                    skip = 0;
                    break;
            }

            string[] daysOfWeek = { "شنبه", "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };
            for (int i = 0; i < 42; i++)
            {
                var calenderSchedulerVM = new CalenderSchedulerVM
                {
                    Index = i,
                    IsActive = false,
                    Name = daysOfWeek[i % 7],
                    IsToday = false
                };
                result.Add(calenderSchedulerVM);
            }

            try
            {
                var now = DateOnly.FromDateTime(DateTime.Now);
                var items = new List<CalenderSchedulerItemVM>();
                var personIdList = new List<Guid>();
               

                for (int i = 0; i < daysInMonth; i++)
                {
                    items = new List<CalenderSchedulerItemVM>();
                    var vm = result.Skip(skip + i).First();
                    var dateTime = persianCalendar.ToDateTime(int.Parse(splitDate[0]), int.Parse(splitDate[1]), i + 1, 0, 0, 0, 0);
                    var dateOnly = DateOnly.FromDateTime(dateTime);
                    if (dateOnly == now)
                        vm.IsToday = true;

                    foreach (var vendorId in searchDto.VendorIds)
                    {
                        var findItems = await _schedulerTaskVendorService.GetAllAsync<CalenderSchedulerItemVM>(
                               x => x.VendorId == vendorId &&
                                    x.Date == dateOnly &&
                                    (string.IsNullOrEmpty(searchDto.SearchTerm) || x.SchedulerTask.Title.Contains(searchDto.SearchTerm)) &&
                                    (!searchDto.Type.HasValue || x.SchedulerTask.Type == searchDto.Type.Value),
                               i => i.Include(x => x.Vendor).ThenInclude(x => x.User)
                                     .Include(x => x.SchedulerTask).ThenInclude(x => x.CreateBy),
                               o => o.OrderBy(x => x.Time),
                               false,
                               cancellationToken);
                    }

                    vm.Day = i + 1;
                    vm.Date = dateOnly;
                    vm.IsActive = true;
                    vm.Items = items;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return result;
        }

        public async Task<bool> AddSchedulerTask(SchedulerTaskDTO schedulerTaskDTO, CancellationToken cancellationToken)
        {
            var result = await AddAsync(schedulerTaskDTO, true, cancellationToken);

            foreach (var person in schedulerTaskDTO.Vendors)
            {
                await _schedulerTaskVendorService.AddAsync(new SchedulerTaskVendorDTO
                {
                    IsDone = false,
                    VendorId = person,
                    SchedulerTaskId = result.Id,
                    Date = result.Date,
                    Description = null,
                    ReminderDateTime = result.ReminderDateTime,
                    Time = result.Time,
                }, true, cancellationToken);
            }

            if (schedulerTaskDTO.RepetType.HasValue && schedulerTaskDTO.RepeatCount != 0)
            {
                DateOnly date = schedulerTaskDTO.Date;
                DateTime? reminder = schedulerTaskDTO.ReminderDateTime;

                for (int i = 0; i < schedulerTaskDTO.RepeatCount; i++)
                {
                    switch (schedulerTaskDTO.RepetType)
                    {
                        case SchedulerTaskRepetType.Daily:
                            date = date.AddDays(1);
                            reminder = reminder?.AddDays(1);
                            break;

                        case SchedulerTaskRepetType.Weekly:
                            date = date.AddDays(7);
                            reminder = reminder?.AddDays(7);
                            break;

                        case SchedulerTaskRepetType.Monthly:
                            AddPersianMonths(ref date, ref reminder, 1);
                            break;

                        case SchedulerTaskRepetType.Every3Month:
                            AddPersianMonths(ref date, ref reminder, 3);
                            break;

                        case SchedulerTaskRepetType.Every6Month:
                            AddPersianMonths(ref date, ref reminder, 6);
                            break;

                        case SchedulerTaskRepetType.Yearly:
                            AddPersianMonths(ref date, ref reminder, 12);
                            break;
                    }

                    var repetTask = await AddAsync(new SchedulerTaskDTO()
                    {
                        Time = schedulerTaskDTO.Time,
                        Date = date,
                        Description = schedulerTaskDTO.Description,
                        Title = schedulerTaskDTO.Title,
                        Priority = schedulerTaskDTO.Priority,
                        Type = schedulerTaskDTO.Type,
                        RepetType = null,
                        ReminderDateTime = reminder,
                        RepeatCount = 0,
                    }, true, cancellationToken);

                    foreach (var vendor in schedulerTaskDTO.Vendors)
                    {
                        await _schedulerTaskVendorService.AddAsync(new SchedulerTaskVendorDTO
                        {
                            IsDone = false,
                            VendorId = vendor,
                            SchedulerTaskId = repetTask.Id,
                            Time = repetTask.Time,
                            ReminderDateTime = repetTask.ReminderDateTime,
                            Description = null,
                            Date = repetTask.Date,
                        }, true, cancellationToken);
                    }
                }
            }

            return true;
        }

        private void AddPersianMonths(ref DateOnly date, ref DateTime? reminder, int monthsToAdd)
        {
            // تاریخ اصلی
            DateTime dateTime = date.ToDateTime(TimeOnly.MinValue);
            PersianCalendar pc = new PersianCalendar();

            int year = pc.GetYear(dateTime);
            int month = pc.GetMonth(dateTime);
            int day = pc.GetDayOfMonth(dateTime);

            month += monthsToAdd;
            while (month > 12)
            {
                month -= 12;
                year += 1;
            }

            int daysInMonth = pc.GetDaysInMonth(year, month);
            if (day > daysInMonth)
                day = daysInMonth;

            dateTime = pc.ToDateTime(year, month, day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
            date = DateOnly.FromDateTime(dateTime);

            // تاریخ یادآور
            if (reminder.HasValue)
            {
                DateTime reminderValue = reminder.Value;

                int rYear = pc.GetYear(reminderValue);
                int rMonth = pc.GetMonth(reminderValue);
                int rDay = pc.GetDayOfMonth(reminderValue);

                rMonth += monthsToAdd;
                while (rMonth > 12)
                {
                    rMonth -= 12;
                    rYear += 1;
                }

                int rDaysInMonth = pc.GetDaysInMonth(rYear, rMonth);
                if (rDay > rDaysInMonth)
                    rDay = rDaysInMonth;

                reminder = pc.ToDateTime(rYear, rMonth, rDay,
                    reminderValue.Hour, reminderValue.Minute, reminderValue.Second, reminderValue.Millisecond);
            }
        }

        public async Task<bool> EditSchedulerTask(SchedulerTaskDTO schedulerTaskDTO, CancellationToken cancellationToken)
        {
            //UiValidationException validationExceptions = new UiValidationException(ResultType.Error);
            //if (schedulerTaskDTO.Vendors == null || !schedulerTaskDTO.Vendors.Any())
            //{
            //    validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.AssignToCantBeEmpty);
            //    throw validationExceptions;
            //}

            //var currentUserId = Utility.SystemSettingsProvider.GetCurrentUserId();
            var schedulerTask = await GetAsync<SchedulerTaskDTO>(x => x.Id == schedulerTaskDTO.Id, null, false, cancellationToken);

            //if (schedulerTask == null)
            //{
            //    validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.EditByCreator);
            //    throw validationExceptions;
            //}

            var previousDate = schedulerTask.Date;
            var previousTime = schedulerTaskDTO.Time;
            var previousReminderDateTime = schedulerTaskDTO.ReminderDateTime;

            schedulerTask = _mapper.Map(schedulerTaskDTO, schedulerTask);
            var result = await UpdateAsync(schedulerTask, true, true, cancellationToken);

            var schedulerTaskVendors = await _schedulerTaskVendorService.GetAllAsync<SchedulerTaskVendorDTO>(x => x.SchedulerTaskId == schedulerTaskDTO.Id, null, null, false, cancellationToken);

            var deleted = schedulerTaskVendors.Where(x => !schedulerTaskDTO.Vendors.Contains(x.VendorId)).ToList();
            foreach (var item in deleted)
            {
                await _schedulerTaskVendorService.DeleteAsync(item.Id, true, true, true, cancellationToken);
            }

            var edited = schedulerTaskVendors.Where(x => schedulerTaskDTO.Vendors.Contains(x.VendorId)).ToList();
            foreach (var item in edited)
            {

                if (item.Date == previousDate) item.Date = result.Date;
                if (item.Time == previousTime) item.Time = result.Time;
                if (item.ReminderDateTime == previousReminderDateTime) item.ReminderDateTime = result.ReminderDateTime;

                await _schedulerTaskVendorService.UpdateAsync(item, true, true, cancellationToken);
            }

            var added = schedulerTaskDTO.Vendors.Where(x => !schedulerTaskVendors.Select(x => x.VendorId).Contains(x)).ToList();
            foreach (var item in added)
            {
                await _schedulerTaskVendorService.AddAsync(new SchedulerTaskVendorDTO
                {
                    VendorId = item,
                    SchedulerTaskId = schedulerTaskDTO.Id,
                    Date = result.Date,
                    Description = null,
                    IsDone = false,
                    ReminderDateTime = result.ReminderDateTime,
                    Time = result.Time,
                }, true, cancellationToken);
            }

            return result != null;
        }

        public async Task<bool> DeleteSchedulerTask(SchedulerTaskDTO schedulerTaskDTO, CancellationToken cancellationToken)
        {
            UiValidationException validationExceptions = new UiValidationException(ResultType.Error);

            //var currentUserId = Utility.SystemSettingsProvider.GetCurrentUserId();
            //var schedulerTask = await FindModelAsync<SchedulerTaskDTO>(x => x.Id == schedulerTaskDTO.Id && x.CreateById == currentUserId, null, false, cancellationToken);
            //if (schedulerTask == null)
            //{
            //    validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.EditByCreator);
            //    throw validationExceptions;
            //}

            var result = await DeleteAsync(schedulerTaskDTO.Id, true, true, true, cancellationToken);

            var schedulerTaskVendors = await _schedulerTaskVendorService.GetAllAsync<SchedulerTaskVendorDTO>(x => x.SchedulerTaskId == schedulerTaskDTO.Id, null, null, false, cancellationToken);
            foreach (var item in schedulerTaskVendors)
            {
                await _schedulerTaskVendorService.DeleteAsync(item.Id, true, true, true, cancellationToken);
            }

            return result;
        }

    }
}
