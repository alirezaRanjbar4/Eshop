using Eshop.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eshop.Common.Helpers.Utilities.Utilities.Providers
{
    public class CalandarHelper
    {
        private UserCulture _userCulture;
        public CalandarHelper()
        {
            _userCulture = UserCulture.Fa;
        }
        public CalandarHelper(UserCulture userCulture) : this()
        {
            _userCulture = userCulture;
        }

        #region Property
        //TODO:Farhadnia:REFACTOR
        private static string Shanbeh = "شنبه";
        private static string Yekshanbeh = "یکشنبه";
        private static string Doshanbeh = "دوشنبه";
        private static string Seshanbeh = "سه شنبه";
        private static string Chaharshanbeh = "چهارشنبه";
        private static string Panjshanbeh = "پنجشنبه";
        private static string Jomeh = "جمعه";
        static int MinutPerDay = 1440;
        static int MinutePerHour = 60;
        #endregion


        public string MiladiToShamsi(DateTime date)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                var year = pc.GetYear(date);
                var month = pc.GetMonth(date);
                var day = pc.GetDayOfMonth(date);
                return
                    $"{year.ToString().PadLeft(4, '0')}/{month.ToString().PadLeft(2, '0')}/{day.ToString().PadLeft(2, '0')}";
            }
            catch (Exception e)
            {
                throw new Exception($"Sended date: {date} {e.Message}");
            }
        }

        public string UTCToShamsiWithIranTime(DateTime utcDate, bool containsSecond = true)
        {
            try
            {
                // تبدیل تاریخ و زمان ورودی به زمان محلی ایران
                var iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                var iranDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, iranTimeZone);

                // از زمان یک ساعت (۶۰ دقیقه) کم کنید
                // iranDateTime = iranDateTime.AddMinutes(-60);

                // تبدیل تاریخ محلی ایران به تاریخ شمسی
                PersianCalendar pc = new PersianCalendar();
                var shamsiYear = pc.GetYear(iranDateTime);
                var shamsiMonth = pc.GetMonth(iranDateTime);
                var shamsiDay = pc.GetDayOfMonth(iranDateTime);

                // استخراج ساعت، دقیقه و ثانیه
                var hour = iranDateTime.Hour;
                var minute = iranDateTime.Minute;
                var second = iranDateTime.Second;

                if (containsSecond)
                    return $"{hour.ToString("00")}:{minute.ToString("00")}:{second.ToString("00")} - {shamsiYear.ToString().PadLeft(4, '0')}/{shamsiMonth.ToString().PadLeft(2, '0')}/{shamsiDay.ToString().PadLeft(2, '0')}";
                else
                    return $"{hour.ToString("00")}:{minute.ToString("00")} - {shamsiYear.ToString().PadLeft(4, '0')}/{shamsiMonth.ToString().PadLeft(2, '0')}/{shamsiDay.ToString().PadLeft(2, '0')}";
            }
            catch (Exception e)
            {
                throw new Exception($"Sent UTC date: {utcDate} {e.Message}");
            }
        }

        public string UTCToShamsiWithIranTimeOnlyDate(DateTime utcDate)
        {
            try
            {
                // تبدیل تاریخ و زمان ورودی به زمان محلی ایران
                var iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                var iranDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, iranTimeZone);

                // تبدیل تاریخ محلی ایران به تاریخ شمسی
                PersianCalendar pc = new PersianCalendar();
                var shamsiYear = pc.GetYear(iranDateTime);
                var shamsiMonth = pc.GetMonth(iranDateTime);
                var shamsiDay = pc.GetDayOfMonth(iranDateTime);

                return $"{shamsiYear.ToString().PadLeft(4, '0')}/{shamsiMonth.ToString().PadLeft(2, '0')}/{shamsiDay.ToString().PadLeft(2, '0')}";
            }
            catch (Exception e)
            {
                throw new Exception($"Sent UTC date: {utcDate} {e.Message}");
            }
        }

        public string UTCToShamsiWithIranTimeOnlyTime(DateTime utcDate, bool containsSecond = true)
        {
            try
            {
                // تبدیل تاریخ و زمان ورودی به زمان محلی ایران
                var iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                var iranDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, iranTimeZone);

                // استخراج ساعت، دقیقه و ثانیه
                var hour = iranDateTime.Hour;
                var minute = iranDateTime.Minute;
                var second = iranDateTime.Second;

                if (containsSecond)
                    return $"{hour.ToString("00")}:{minute.ToString("00")}:{second.ToString("00")}";
                else
                    return $"{hour.ToString("00")}:{minute.ToString("00")}";
            }
            catch (Exception e)
            {
                throw new Exception($"Sent UTC date: {utcDate} {e.Message}");
            }
        }

        public DateTime ShamsiToMiladi(string jalaliDate)
        {
            try
            {
                string[] strs = jalaliDate.Split('/');
                PersianCalendar pc = new PersianCalendar();
                if (strs[0].Length == 4)
                {
                    return pc.ToDateTime(Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[2]), 0, 0, 0, 0);
                }
                else
                {
                    return pc.ToDateTime(Convert.ToInt32(strs[2]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[0]), 0, 0, 0, 0);
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sended date: {0} {1}", jalaliDate, e.Message));
            }
        }

        public string CalculateTimeRemaining(DateTime targetDateTime)
        {

            DateTime currentTime = DateTime.Now;
            TimeSpan timeRemaining = targetDateTime - currentTime;

            // نمایش زمان باقیمانده به صورت کلی
            // string result = "زمان باقیمانده: " + timeRemaining.ToString(@"dd\:hh\:mm\:ss");

            int days = timeRemaining.Days;
            int hours = timeRemaining.Hours;
            int minutes = timeRemaining.Minutes;
            int seconds = timeRemaining.Seconds;

            string result = $"\nروز: {days}, ساعت‌: {hours}, دقیقه‌: {minutes}";

            return result;
        }








        /// <summary>
        /// تبدیل تاریخ شمسی به میلادی و برعکس بر اساس زبان تعیین شده کاربر
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string ConvertDataByCulture(string date)
        {
            var Date = DateTime.Parse(date);
            if (_userCulture == UserCulture.Fa)
            {
                return MiladiToShamsi(Date);
            }
            return Date.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// تبدیل تاریخ به تایپ تاریخ دیتابیس بر اساس زبان کاربر
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime ConvertStringDateToDbDate(string date)
        {
            if (_userCulture == UserCulture.Fa)
            {
                return ShamsiToMiladi(date);
            }
            return Convert.ToDateTime(date);
        }

        public DateTime ConvertStringDataByCulture(string date)
        {
            switch (_userCulture)
            {
                case UserCulture.Fa:
                    //CalendarType=Jalali
                    return JalaliToGregorianDateTime(date);

                case UserCulture.En:
                    //CalendarType=Gregorian
                    return DateTime.Parse(date);

                default:
                    return DateTime.Parse(date);
            }

        }

        /// <summary>
        /// میلادی به شمسی
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns>تاریه شمسی</returns>
        public string MiladiToShamsiToFullDateTimeString(DateTime date)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                var year = pc.GetYear(date);
                var month = pc.GetMonth(date);
                var day = pc.GetDayOfMonth(date);
                return
                    $"{date.ToShortTimeString()} - {year.ToString().PadLeft(4, '0')}/{month.ToString().PadLeft(2, '0')}/{day.ToString().PadLeft(2, '0')}";
            }
            catch (Exception e)
            {
                throw new Exception($"Sended date: {date} {e.Message}");
            }
        }

        public string GetLanguageTypeByCalendarType(CalendarType calendarType)
        {
            try
            {
                string languageType = "";
                switch (calendarType)
                {
                    case CalendarType.Jalali:
                        languageType = "fa-IR";
                        break;
                    case CalendarType.Gregorian:
                        languageType = "en-US";
                        break;
                    default:
                        break;
                }
                return languageType;
            }
            catch (Exception e)
            {
                throw new Exception($"Sended type: {calendarType} {e.Message}");
            }
        }

        /// <summary>
        /// شمسی به میلادی
        /// </summary>
        /// <param name="date">تاریخ شمسی</param>
        /// <returns>تاریخ میلادی</returns>
        public string ShamsiToMiladiToString(string date)
        {
            try
            {
                return ShamsiToMiladi(date).ToString("yyyy/MM/dd");
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sended date: {0} {1}", date, e.Message));
            }
        }

        /// <summary>
        /// میلادی متنی به میلادی
        /// </summary>
        /// <param name="date">تاریخ میلادی متنی</param>
        /// <returns>تاریخ میلادی</returns>
        public DateTime GregorianStringToGregorianDateTime(string gregorianDate)
        {
            try
            {
                gregorianDate = gregorianDate.Replace("-", "/");
                string[] strs = gregorianDate.Split('/');
                if (strs[0].Length == 4)
                {
                    return new DateTime(Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[2]));
                }
                else
                {
                    return new DateTime(Convert.ToInt32(strs[2]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[0]));
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sended date : {0} {1}", gregorianDate, e.Message));
            }
        }

        /// <summary>
        /// آغاز شروع و پایان هفته میلادی
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns>تاریخ اول هفته و تاریخ پایان هفته</returns>
        public Tuple<DateTime, DateTime> GetCurrentWeekMiladi(DateTime date)
        {
            try
            {
                var start = date.Date.AddDays(-(int)date.DayOfWeek); // prev sunday 00:00
                var end = start.AddDays(6); // next sunday 00:00

                return new Tuple<DateTime, DateTime>(start, end);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sended date: {0} {1}", date, e.Message));
            }
        }

        /// <summary>
        /// آغاز شروع و پایان هفته میلادی
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns>تاریخ اول هفته و تاریخ پایان هفته</returns>
        public Tuple<DateTime, DateTime> GetCurrentWeekMiladi(DateTime date, DayOfWeek startOfWeek)
        {
            try
            {
                var start = date.Date.AddDays(-(int)startOfWeek); // prev sunday 00:00
                var end = start.AddDays(6); // next sunday 00:00

                return new Tuple<DateTime, DateTime>(start, end);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sended date: {0} {1}", date, e.Message));
            }
        }

        /// <summary>
        /// آغاز شروع و پایان هفته شمسی
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns>تاریخ اول هفته و تاریخ پایان هفته</returns>
        public Tuple<DateTime, DateTime> GetCurrentWeekShamsi(DateTime date)
        {
            try
            {
                //var persianDate = Convert.ToDateTime(MiladiToShamsi(date));
                //PersianCalendar persianCalendar = new PersianCalendar();
                //var startPersian = persianCalendar.AddDays(persianDate, -GetDayOfWeekPersian(persianDate.DayOfWeek));
                //var start = ShamsiToMiladi(startPersian.Year + "/" + startPersian.Month + "/" + startPersian.Day);
                //var end = start.AddDays(6); // next Friday 00:00

                //return new Tuple<DateTime, DateTime>(start, end);

                //PersianCalendar persianCalendar = new PersianCalendar();
                //var startPersian = persianCalendar.AddDays(date, -GetDayOfWeekPersian(persianCalendar.GetDayOfWeek(date)));
                //var start = ShamsiToMiladi(startPersian.Year + "/" + startPersian.Month + "/" + startPersian.Day);
                //var end = start.AddDays(6); // next Friday 00:00

                //return new Tuple<DateTime, DateTime>(start, end);

                PersianCalendar persianCalendar = new PersianCalendar();
                var startPersian = persianCalendar.AddDays(date, -GetDayOfWeekPersian(persianCalendar.GetDayOfWeek(date)));
                var start = Convert.ToDateTime(startPersian.Year + "/" + startPersian.Month + "/" + startPersian.Day);
                var end = start.AddDays(6); // next Friday 00:00

                return new Tuple<DateTime, DateTime>(start, end);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Sended date: {0} {1}", date, e.Message));
            }
        }

        public Tuple<DateTime, DateTime> GetCurrentWeekByCulture(DateTime date)
        {

            switch (_userCulture)
            {
                case UserCulture.Fa:
                    return GetCurrentWeekShamsi(date);

                case UserCulture.En:
                    return GetCurrentWeekMiladi(date);

                default:
                    return GetCurrentWeekShamsi(date);

            }

        }

        /// <summary>
        /// نام روز هفته به فارسی
        /// </summary>
        /// <param name="WeekDay"></param>
        /// <returns></returns>
        public string GetPersianDayName(DayOfWeek WeekDay)
        {
            switch (WeekDay)
            {
                case DayOfWeek.Saturday:
                    return Shanbeh;

                case DayOfWeek.Sunday:
                    return Yekshanbeh;

                case DayOfWeek.Monday:
                    return Doshanbeh;

                case DayOfWeek.Tuesday:
                    return Seshanbeh;

                case DayOfWeek.Wednesday:
                    return Chaharshanbeh;

                case DayOfWeek.Thursday:
                    return Panjshanbeh;

                case DayOfWeek.Friday:
                    return Jomeh;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string GetEnglishDayName(DayOfWeek WeekDay)
        {
            switch (WeekDay)
            {
                case DayOfWeek.Saturday:
                    return DayOfWeek.Saturday.ToString();

                case DayOfWeek.Sunday:
                    return DayOfWeek.Sunday.ToString();

                case DayOfWeek.Monday:
                    return DayOfWeek.Monday.ToString();

                case DayOfWeek.Tuesday:
                    return DayOfWeek.Tuesday.ToString();

                case DayOfWeek.Wednesday:
                    return DayOfWeek.Wednesday.ToString();

                case DayOfWeek.Thursday:
                    return DayOfWeek.Thursday.ToString();

                case DayOfWeek.Friday:
                    return DayOfWeek.Friday.ToString();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// نام روز هفته به فارسی
        /// </summary>
        /// <param name="WeekDay"></param>
        /// <returns></returns>
        public int GetDayOfWeekPersian(DayOfWeek WeekDay)
        {
            switch (WeekDay)
            {
                case DayOfWeek.Saturday:
                    return 0;

                case DayOfWeek.Sunday:
                    return 1;

                case DayOfWeek.Monday:
                    return 2;

                case DayOfWeek.Tuesday:
                    return 3;

                case DayOfWeek.Wednesday:
                    return 4;

                case DayOfWeek.Thursday:
                    return 5;

                case DayOfWeek.Friday:
                    return 6;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// بعد از ایجاد ریسورس ها در پروژه باید از حالت Comment خارج شود و مقادیر صحیح را اعمال شود.
        //public   string GetCurrentDateTimeLocalDateTime(Enums.CalendarType calendarType, Enums.LanguageType languageType)
        //{
        //    string dayName = string.Empty;
        //    string monthName = string.Empty;

        //    bool isSysCultureEqualsUiCulture = false;
        //    Calendar calendar = null;

        //    switch (languageType)
        //    {
        //        case Enums.LanguageType.fa_IR:
        //            switch (calendarType)
        //            {
        //                case Enums.CalendarType.Jalali:
        //                    isSysCultureEqualsUiCulture = true;
        //                    calendar = new PersianCalendar();
        //                    break;
        //                case Enums.CalendarType.Gregorian:
        //                    isSysCultureEqualsUiCulture = false;
        //                    calendar = new GregorianCalendar();
        //                    break;
        //            }
        //            break;
        //        case Enums.LanguageType.en_US:
        //            switch (calendarType)
        //            {
        //                case Enums.CalendarType.Jalali:
        //                    isSysCultureEqualsUiCulture = false;
        //                    calendar = new PersianCalendar();
        //                    break;
        //                case Enums.CalendarType.Gregorian:
        //                    isSysCultureEqualsUiCulture = true;
        //                    calendar = new GregorianCalendar();
        //                    break;
        //            }
        //            break;
        //        default:
        //            calendar = new GregorianCalendar();
        //            break;
        //    }

        //    int year = calendar.GetYear(DateTime.Now);
        //    int month = calendar.GetMonth(DateTime.Now);
        //    int day = calendar.GetDayOfMonth(DateTime.Now);

        //    switch (month)
        //    {
        //        case 1:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month1E : GlobalResources.App.Month1NE; break;
        //        case 2:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month2E : GlobalResources.App.Month2NE; ; break;
        //        case 3:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month3E : GlobalResources.App.Month3NE; ; break;
        //        case 4:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month4E : GlobalResources.App.Month4NE; ; break;
        //        case 5:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month5E : GlobalResources.App.Month5NE; ; break;
        //        case 6:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month6E : GlobalResources.App.Month6NE; ; break;
        //        case 7:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month7E : GlobalResources.App.Month7NE; ; break;
        //        case 8:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month8E : GlobalResources.App.Month8NE; ; break;
        //        case 9:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month9E : GlobalResources.App.Month9NE; ; break;
        //        case 10:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month10E : GlobalResources.App.Month10NE; ; break;
        //        case 11:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month11E : GlobalResources.App.Month11NE; ; break;
        //        case 12:
        //            monthName = isSysCultureEqualsUiCulture ? GlobalResources.App.Month12E : GlobalResources.App.Month12NE; ; break;
        //    }

        //    return string.Format("{0} {1} {2}", day, monthName, year);
        //}


        /// بعد از ایجاد ریسورس ها در پروژه باید از حالت Comment خارج شود و مقادیر صحیح را اعمال شود.

        //public   string GetCurrentDateTimeLocalDayOfWeek(Enums.CalendarType calendarType, Enums.LanguageType languageType, DateTime dateTime)
        //{
        //    string dayName = string.Empty;
        //    bool isSysCultureEqualsUiCulture = false;
        //    Calendar calendar;

        //    switch (languageType)
        //    {
        //        case Enums.LanguageType.fa_IR:
        //            calendar = new PersianCalendar();
        //            switch (calendarType)
        //            {
        //                case Enums.CalendarType.Jalali: isSysCultureEqualsUiCulture = true; break;
        //                case Enums.CalendarType.Gregorian: isSysCultureEqualsUiCulture = false; break;
        //            }
        //            break;
        //        case Enums.LanguageType.en_US:
        //            calendar = new GregorianCalendar();
        //            switch (calendarType)
        //            {
        //                case Enums.CalendarType.Jalali: isSysCultureEqualsUiCulture = false; break;
        //                case Enums.CalendarType.Gregorian: isSysCultureEqualsUiCulture = true; break;
        //            }
        //            break;
        //        default:
        //            calendar = new GregorianCalendar();
        //            break;
        //    }

        //    DayOfWeek dayOfWeek = calendar.GetDayOfWeek(dateTime);

        //    switch (GetDayOfWeekLocalDateTime(dayOfWeek, languageType))
        //    {
        //        case 1:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day1E : GlobalResources.App.Day1NE;
        //            break;
        //        case 2:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day2E : GlobalResources.App.Day2NE;
        //            break;
        //        case 3:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day3E : GlobalResources.App.Day3NE;
        //            break;
        //        case 4:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day4E : GlobalResources.App.Day4NE;
        //            break;
        //        case 5:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day5E : GlobalResources.App.Day5NE;
        //            break;
        //        case 6:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day6E : GlobalResources.App.Day6NE;
        //            break;
        //        case 7:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day7E : GlobalResources.App.Day7NE;
        //            break;
        //    }
        //    return dayName;
        //}
        private int GetDayOfWeekLocalDateTime(DayOfWeek DayNumber, LanguageType languageType)
        {
            int dayOfWeek = -1;

            switch (languageType)
            {
                case LanguageType.fa_IR:
                    switch (DayNumber)
                    {
                        case DayOfWeek.Friday:
                            dayOfWeek = 7;
                            break;
                        case DayOfWeek.Monday:
                            dayOfWeek = 3;
                            break;
                        case DayOfWeek.Saturday:
                            dayOfWeek = 1;
                            break;
                        case DayOfWeek.Sunday:
                            dayOfWeek = 2;
                            break;
                        case DayOfWeek.Thursday:
                            dayOfWeek = 6;
                            break;
                        case DayOfWeek.Tuesday:
                            dayOfWeek = 4;
                            break;
                        case DayOfWeek.Wednesday:
                            dayOfWeek = 5;
                            break;
                    }
                    break;
                case LanguageType.en_US:
                    switch (DayNumber)
                    {
                        case DayOfWeek.Friday:
                            dayOfWeek = 5;
                            break;
                        case DayOfWeek.Monday:
                            dayOfWeek = 1;
                            break;
                        case DayOfWeek.Saturday:
                            dayOfWeek = 6;
                            break;
                        case DayOfWeek.Sunday:
                            dayOfWeek = 7;
                            break;
                        case DayOfWeek.Thursday:
                            dayOfWeek = 4;
                            break;
                        case DayOfWeek.Tuesday:
                            dayOfWeek = 2;
                            break;
                        case DayOfWeek.Wednesday:
                            dayOfWeek = 3;
                            break;
                    }
                    break;
            }

            return dayOfWeek;
        }

        /// به دلیل عدم ایجاد ریسورس ها در پروژه کامت شد که باید از کانت خارج شود

        //گرفتن نام روز هفته
        //public   string GetWeekDayOfDate(DateTime date, Enums.CalendarType calendarType, Enums.LanguageType languageType)
        //{
        //    bool isSysCultureEqualsUiCulture = false;
        //    string dayName = string.Empty;
        //    Calendar calendar;
        //    switch (languageType)
        //    {
        //        case Enums.LanguageType.fa_IR:
        //            switch (calendarType)
        //            {
        //                case Enums.CalendarType.Jalali:
        //                    isSysCultureEqualsUiCulture = true;
        //                    calendar = new PersianCalendar();
        //                    break;
        //                case Enums.CalendarType.Gregorian:
        //                    isSysCultureEqualsUiCulture = false;
        //                    calendar = new GregorianCalendar();
        //                    break;
        //                default:
        //                    calendar = new GregorianCalendar();
        //                    break;
        //            }
        //            break;

        //        case Enums.LanguageType.en_US:
        //            switch (calendarType)
        //            {
        //                case Enums.CalendarType.Jalali:
        //                    isSysCultureEqualsUiCulture = false;
        //                    calendar = new PersianCalendar();
        //                    break;
        //                case Enums.CalendarType.Gregorian:
        //                    isSysCultureEqualsUiCulture = true;
        //                    calendar = new GregorianCalendar();
        //                    break;
        //                default:
        //                    calendar = new GregorianCalendar();
        //                    break;
        //            }
        //            break;
        //        default:
        //            calendar = new GregorianCalendar();
        //            break;

        //    }
        //    int year = calendar.GetYear(date);
        //    int month = calendar.GetMonth(date);
        //    DayOfWeek day = calendar.GetDayOfWeek(date);
        //    switch (GetDayOfWeekLocalDateTime(day, calendarType))
        //    {
        //        case 1:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day1E : GlobalResources.App.Day1NE;
        //            break;
        //        case 2:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day2E : GlobalResources.App.Day2NE;
        //            break;
        //        case 3:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day3E : GlobalResources.App.Day3NE;
        //            break;
        //        case 4:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day4E : GlobalResources.App.Day4NE;
        //            break;
        //        case 5:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day5E : GlobalResources.App.Day5NE;
        //            break;
        //        case 6:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day6E : GlobalResources.App.Day6NE;
        //            break;
        //        case 7:
        //            dayName = isSysCultureEqualsUiCulture ? GlobalResources.App.Day7E : GlobalResources.App.Day7NE;
        //            break;
        //    }
        //    return dayName;
        //}
        private int GetDayOfWeekLocalDateTime(DayOfWeek DayNumber, CalendarType calendarType)
        {
            int dayOfWeek = -1;

            switch (calendarType)
            {
                case CalendarType.Jalali:
                    switch (DayNumber)
                    {
                        case DayOfWeek.Friday:
                            dayOfWeek = 7;
                            break;
                        case DayOfWeek.Monday:
                            dayOfWeek = 3;
                            break;
                        case DayOfWeek.Saturday:
                            dayOfWeek = 1;
                            break;
                        case DayOfWeek.Sunday:
                            dayOfWeek = 2;
                            break;
                        case DayOfWeek.Thursday:
                            dayOfWeek = 6;
                            break;
                        case DayOfWeek.Tuesday:
                            dayOfWeek = 4;
                            break;
                        case DayOfWeek.Wednesday:
                            dayOfWeek = 5;
                            break;
                    }
                    break;
                case CalendarType.Gregorian:
                    switch (DayNumber)
                    {
                        case DayOfWeek.Friday:
                            dayOfWeek = 5;
                            break;
                        case DayOfWeek.Monday:
                            dayOfWeek = 1;
                            break;
                        case DayOfWeek.Saturday:
                            dayOfWeek = 6;
                            break;
                        case DayOfWeek.Sunday:
                            dayOfWeek = 7;
                            break;
                        case DayOfWeek.Thursday:
                            dayOfWeek = 4;
                            break;
                        case DayOfWeek.Tuesday:
                            dayOfWeek = 2;
                            break;
                        case DayOfWeek.Wednesday:
                            dayOfWeek = 3;
                            break;
                    }
                    break;
            }
            return dayOfWeek;
        }

        public bool ISValidDate(string date, CalendarType calendarType)
        {
            try
            {
                DateTime startDate;
                switch (calendarType)
                {
                    case CalendarType.Jalali:
                        string[] strs = date.Split('/');
                        PersianCalendar persianCalendar = new PersianCalendar();
                        startDate = persianCalendar.ToDateTime(Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[2]), 0, 0, 0, 0);
                        break;
                    case CalendarType.Gregorian:
                        startDate = DateTime.Parse(date);
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// گرفتن تاریخ اولین روز از ماه و سال انتخابی بر اساس  تقویم
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="calendarType"></param>
        /// <returns></returns>            
        public DateTime GetStartDateOfMonth(int year, int month, CalendarType calendarType)
        {
            DateTime startDate;
            switch (calendarType)
            {
                case CalendarType.Jalali:
                    PersianCalendar persianCalendar = new PersianCalendar();
                    startDate = persianCalendar.ToDateTime(year, month, 1, 0, 0, 0, 0);
                    break;
                case CalendarType.Gregorian:
                    startDate = DateTime.Parse(year + "/" + month + "/" + "01");
                    break;
                default:
                    startDate = DateTime.Parse(year + "/" + month + "/" + "01");
                    break;
            }
            return startDate;
        }

        /// <summary>
        ///گرفتن تاریخ اخرین روز از ماه و سال انتخابی بر اساس  تقویم
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="calendarType"></param>
        /// <returns></returns>
        public DateTime GetEndDateOfMonth(int year, int month, CalendarType calendarType)
        {
            DateTime endDate;
            int lastDayInMonth = 0;
            switch (calendarType)
            {
                case CalendarType.Jalali:
                    PersianCalendar persianCalendar = new PersianCalendar();
                    lastDayInMonth = persianCalendar.GetDaysInMonth(year, month);
                    endDate = persianCalendar.ToDateTime(year, month, lastDayInMonth, 0, 0, 0, 0);
                    break;
                case CalendarType.Gregorian:
                    lastDayInMonth = DateTime.DaysInMonth(year, month);
                    endDate = DateTime.Parse(year + "/" + month + "/" + lastDayInMonth);
                    break;
                default:
                    lastDayInMonth = DateTime.DaysInMonth(year, month);
                    endDate = DateTime.Parse(year + "/" + month + "/" + lastDayInMonth);
                    break;
            }
            return endDate;
        }

        public DateTime JalaliToGregorian(int year, int month, int day)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public DateTime GetEndOfYear(int year, int month, CalendarType calendarType)
        {
            DateTime endDate;
            int lastDayInMonth = 0;
            int LastMontInYear = 0;

            switch (calendarType)
            {
                case CalendarType.Jalali:
                    PersianCalendar persianCalendar = new PersianCalendar();
                    lastDayInMonth = persianCalendar.GetDaysInMonth(year, month);
                    LastMontInYear = persianCalendar.GetMonthsInYear(year, month);
                    endDate = persianCalendar.ToDateTime(year, LastMontInYear, lastDayInMonth - 1, 0, 0, 0, 0);

                    break;
                case CalendarType.Gregorian:
                    GregorianCalendar calendar = new GregorianCalendar();
                    lastDayInMonth = DateTime.DaysInMonth(year, month);
                    LastMontInYear = calendar.GetMonthsInYear(year, month);
                    endDate = DateTime.Parse(year + "/" + LastMontInYear + "/" + lastDayInMonth);
                    break;

                default:
                    GregorianCalendar GregorianCalendar = new GregorianCalendar();
                    lastDayInMonth = DateTime.DaysInMonth(year, month);
                    LastMontInYear = GregorianCalendar.GetMonthsInYear(year, month);
                    endDate = DateTime.Parse(year + "/" + LastMontInYear + "/" + lastDayInMonth);
                    break;


            }
            return endDate;

        }

        public DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        /// <summary>
        ///   تبدیل زمان استرینگی به دقیقه
        /// </summary>
        /// <param name="Time"> زمان استرینگی</param>
        /// <returns>زمان به دقیقه</returns>
        public int ConvertTimeStringToMinute(string Time)
        {
            try
            {


                int houre = 0;
                int minute = 0;
                if (!string.IsNullOrEmpty(Time))
                {
                    houre = Convert.ToInt32(Time.Split(':')[0]);
                    minute = Convert.ToInt32(Time.Split(':')[1]);
                }
                return houre * 60 + minute;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// تبدیل دقیقه به زمان استرینگ
        /// </summary>
        /// <param name="Time">دقیقه</param>
        /// <returns>زمان استرینگی</returns>
        public string ConvertMinuteToTimeString(int Time)
        {
            try
            {
                int hour = 0;
                int minute = 0;
                string hourStr = string.Empty;
                string minitueStr = string.Empty;
                if (Time != 0)
                {
                    hour = Time / 60;
                    minute = Time % 60;
                }
                hourStr = hour < 10 ? "0" + hour.ToString() : hour.ToString();
                minitueStr = minute < 10 ? "0" + minute.ToString() : minute.ToString();

                return hourStr + ":" + minitueStr;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        ///  گرفتن اخرین روز از ماه و سال بر اساس تقویم
        /// </summary>
        /// <param name="calendarType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>        
        public int GetEndDayOfMonth(CalendarType calendarType, int year, int month)
        {
            try
            {
                int lastDayInMonth = 0;
                switch (calendarType)
                {
                    case CalendarType.Jalali:
                        PersianCalendar persianCalendar = new PersianCalendar();
                        lastDayInMonth = persianCalendar.GetDaysInMonth(year, month);
                        break;
                    case CalendarType.Gregorian:
                        lastDayInMonth = DateTime.DaysInMonth(year, month);
                        break;
                }
                return lastDayInMonth;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// گرفتن تاریخ با توجه به روز, ماه وسال بر اساس تقویم      
        /// </summary>
        /// <param name="calendarType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public DateTime GetDateAccordinateYearMonthDay(CalendarType calendarType, int year, int month, int day)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                GregorianCalendar gc = new GregorianCalendar();
                DateTime Date;
                switch (calendarType)
                {
                    case CalendarType.Jalali:
                        Date = pc.ToDateTime(year, month, day, 0, 0, 0, 0, 0);
                        break;
                    case CalendarType.Gregorian:
                    default:
                        Date = gc.ToDateTime(year, month, day, 0, 0, 0, 0, 0);
                        break;
                }
                return Date;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DayOfWeek GetDayOfWeekByCulture(byte dayNumber)
        {
            var dayOfWeek = DayOfWeek.Saturday;

            switch (dayNumber)
            {
                case 0:
                    dayOfWeek = DayOfWeek.Saturday;
                    break;
                case 1:
                    dayOfWeek = DayOfWeek.Sunday;
                    break;
                case 2:
                    dayOfWeek = DayOfWeek.Monday;
                    break;
                case 3:
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case 4:
                    dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case 5:
                    dayOfWeek = DayOfWeek.Thursday;
                    break;
                case 6:
                    dayOfWeek = DayOfWeek.Friday;
                    break;
            }
            return dayOfWeek;

        }

        public List<DateTime> DaysOfMonth(int year, int month, DayOfWeek day)
        {
            List<DateTime> dates = new List<DateTime>();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                DateTime dt = new DateTime(year, month, i);
                if (dt.DayOfWeek == day)
                {
                    dates.Add(dt);
                }
            }
            return dates;
        }

        public DateTime GTSMinStandardDateTime
        {
            get
            {
                return new DateTime(1900, 1, 1);
            }
        }

        public DateTime ConvertToGregorianByCulture(UserCulture culture, DateTime date)
        {
            if (culture == UserCulture.Fa)
            {
                return JalaliToGregorian(date.Year, date.Month, date.Day);
            }
            return date;
        }

        public DateTime ConvertDateByCulture(UserCulture culture, DateTime date)
        {
            if (culture == UserCulture.Fa)
            {
                var shamsiDate = MiladiToShamsi(date);
                return DateTime.Parse(shamsiDate);
            }
            return date;
        }

        public string ConvertDateByCultureString(DateTime date)
        {
            if (_userCulture == UserCulture.Fa)
            {
                var shamsiDate = MiladiToShamsi(date);
                return shamsiDate;
            }
            return date.ToString("yyyy/MM/dd");
        }

        public string GetDayOfWeekByCulture(DateTime dateTime)
        {
            var Weekday = dateTime.DayOfWeek;
            var _result = "";
            if (_userCulture == UserCulture.Fa)
            {
                return GetPersianDayName(Weekday);
            }
            return GetEnglishDayName(Weekday);
        }

        public int GetEndOfMiladiMonth(int year, int month)
        {
            int endOfMonth = DateTime.DaysInMonth(year, month);
            return endOfMonth;
        }

        /// <summary>
        /// تعداد روز ماههای شمسی را برمیگرداند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int GetEndOfPersianMonth(int year, int month)
        {
            int endOfMonth = new PersianCalendar().GetDaysInMonth(year, month);
            return endOfMonth;
        }

        public string GregorianToJalaliDateTime(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var jalaliDate =
                $"{persianCalendar.GetYear(date).ToString().PadLeft(4, '0')}/{persianCalendar.GetMonth(date).ToString().PadLeft(2, '0')}" +
                $"/{persianCalendar.GetDayOfMonth(date).ToString().PadLeft(2, '0')}" + $" {persianCalendar.GetHour(date).ToString().PadLeft(2, '0')}"
                + $":{persianCalendar.GetMinute(date).ToString().PadLeft(2, '0')}";

            return jalaliDate;
        }

        public IList<int> GetFromYearRange(CalendarType calendarType)
        {

            IList<int> years = new List<int>();
            DateTime dt = DateTime.Now;
            if (calendarType == CalendarType.Gregorian)
            {

                for (int i = dt.Year - 2; i <= dt.Year; i++)
                {
                    years.Add(i);
                }
            }
            else if (calendarType == CalendarType.Jalali)
            {
                var pDate = int.Parse(MiladiToShamsi(dt).Split('/')[0]);

                for (int i = pDate - 2; i <= pDate; i++)
                {
                    years.Add(i);
                }
            }
            return years;

        }

        public string GregorianToJalali(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var jalaliDate =
                $"{persianCalendar.GetYear(date).ToString().PadLeft(4, '0')}/{persianCalendar.GetMonth(date).ToString().PadLeft(2, '0')}" +
                $"/{persianCalendar.GetDayOfMonth(date).ToString().PadLeft(2, '0')}";

            return jalaliDate;
        }

        public DateTime JalaliToGregorianDateTime(string dateTime)
        {
            string[] strs = dateTime.Split('/');
            PersianCalendar pc = new PersianCalendar();

            if (strs.Length != 3)       //  when we don't select dateTimePickerManager, it returns 0:0 
            {
                return DateTime.MinValue;
            }
            else
            {
                if (strs[0].Length == 4)
                {
                    string[] parts = strs[2].Split(' ');

                    if (parts.Length > 1)
                    {
                        string[] timeParts = parts[1].Split(':');
                        string[] secondParts = timeParts[2].Split('.');
                        return pc.ToDateTime(Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]), Convert.ToInt32(parts[0]), Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1])
                            , Convert.ToInt32(secondParts[0]), Convert.ToInt32(secondParts[1]));
                    }
                    else
                    {
                        return pc.ToDateTime(Convert.ToInt32(strs[0]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[2]), 0, 0, 0, 0);
                    }
                }
                else
                {
                    string[] parts = strs[2].Split(' ');

                    if (parts.Length > 1)
                    {
                        string[] timeParts = parts[1].Split(':');
                        string[] secondParts = timeParts[2].Split('.');
                        return pc.ToDateTime(Convert.ToInt32(parts[0]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[0]), Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1])
                            , Convert.ToInt32(secondParts[0]), Convert.ToInt32(secondParts[1]));
                    }
                    else
                    {
                        return pc.ToDateTime(Convert.ToInt32(strs[2]), Convert.ToInt32(strs[1]), Convert.ToInt32(strs[0]), 0, 0, 0, 0);
                    }
                }
            }
        }

        /// <summary>
        /// تاریخ اولین روز سال مشخص شده را برمیگرداند
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        /// 
        public DateTime GetDateOfBeginYear(DateTime Date, CalendarType calendarType)
        {
            switch (calendarType)
            {
                case CalendarType.Jalali:
                    int year = Convert.ToInt32(GregorianToJalali(Date).Substring(0, 4));
                    return JalaliToGregorian(year, 1, 1);
                case CalendarType.Gregorian:
                default:
                    return new DateTime(Date.Year, 1, 1);
            }
        }

        /// <summary>
        /// تاریخ آخرین روز سال مشخص شده را برمیگرداند
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public DateTime GetDateOfEndYear(DateTime Date, CalendarType calendarType)
        {
            switch (calendarType)
            {
                case CalendarType.Jalali:
                    int year = Convert.ToInt32(GregorianToJalali(Date).Substring(0, 4));
                    return JalaliToGregorian(year, 12, GetEndOfPersianMonth(year, 12));
                case CalendarType.Gregorian:
                default:
                    return new DateTime(Date.Year, 12, GetEndOfMiladiMonth(Date.Year, 12));
            }
        }

        public IList<int> GetYearRange(CalendarType calendarType, int beforeCurrentYearRange, int afterCurrentYearRange)
        {

            IList<int> years = new List<int>();
            //int rangeYear = 10;
            DateTime dt = DateTime.Now;
            if (calendarType == CalendarType.Gregorian)
            {

                for (int i = dt.Year - beforeCurrentYearRange; i < dt.Year + afterCurrentYearRange; i++)
                {
                    years.Add(i);
                }
            }
            else if (calendarType == CalendarType.Jalali)
            {
                var pDate = int.Parse(MiladiToShamsi(dt).Split('/')[0]);

                for (int i = pDate - beforeCurrentYearRange; i < pDate + afterCurrentYearRange; i++)
                {
                    years.Add(i);
                }
            }
            return years;

        }

        public IList<int> GetToYearRange(CalendarType calendarType)
        {

            IList<int> years = new List<int>();
            DateTime dt = DateTime.Now;
            if (calendarType == CalendarType.Gregorian)
            {

                for (int i = dt.Year - 1; i <= dt.Year; i++)
                {
                    years.Add(i);
                }
            }
            else if (calendarType == CalendarType.Jalali)
            {
                var pDate = int.Parse(MiladiToShamsi(dt).Split('/')[0]);

                for (int i = pDate - 1; i <= pDate; i++)
                {
                    years.Add(i);
                }
            }
            return years;

        }

        public string GregorianStringDate(DateTime date)
        {
            var GregorianDate =
                $"{date.Year.ToString().PadLeft(4, '0')}/{date.Month.ToString().PadLeft(2, '0')}" +
                $"/{date.Day.ToString().PadLeft(2, '0')}";

            return GregorianDate;
        }

        public DateTime ConvertJalaliStringToDate(string dateTime)
        {
            CultureInfo pr = new CultureInfo("fa-ir");
            DateTime dt = DateTime.ParseExact(dateTime, "yyyy/MM/dd", pr);
            return dt;
        }

        public string GregorianToJalaliByTime(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var time = date.ToShortTimeString();
            if (time.IndexOf("AM") > -1)
            {
                time = time.Remove(time.IndexOf("AM") - 1);
                time = " ق ظ " + time;
            }
            else if (time.IndexOf("PM") > -1)
            {
                time = time.Remove(time.IndexOf("PM") - 1);
                time = " ب ظ " + time;
            }
            var jalaliDate =
                $"{time} {persianCalendar.GetYear(date).ToString().PadLeft(4, '0')}/{persianCalendar.GetMonth(date).ToString().PadLeft(2, '0')}/{persianCalendar.GetDayOfMonth(date).ToString().PadLeft(2, '0')} ";

            return jalaliDate;
        }

        public DateTime GetJalaliStartDateInGregorian(int year)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.ToDateTime(year, 1, 1, 0, 0, 0, 0);
        }

        public DateTime GetJalaliEndDateInGregorian(int year)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int lastDayOfMonth = persianCalendar.GetDaysInMonth(year, 12);
            return persianCalendar.ToDateTime(year, 12, lastDayOfMonth, 0, 0, 0, 0);
        }

        public void GetDateRangeInGregorian(int year, CalendarType calendarType, out DateTime startDate, out DateTime endDate)
        {
            int lastDayOfMonth;
            switch (calendarType)
            {
                case CalendarType.Jalali:
                    PersianCalendar persianCalendar = new PersianCalendar();
                    startDate = persianCalendar.ToDateTime(year, 1, 1, 0, 0, 0, 0);
                    lastDayOfMonth = persianCalendar.GetDaysInMonth(year, 12);
                    endDate = persianCalendar.ToDateTime(year, 12, lastDayOfMonth, 0, 0, 0, 0);
                    break;
                case CalendarType.Gregorian:
                    startDate = DateTime.Parse(year + "/01/01");
                    lastDayOfMonth = DateTime.DaysInMonth(year, 12);
                    endDate = DateTime.Parse(year + "/12/" + lastDayOfMonth);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(calendarType), calendarType, null);
            }
        }

        public bool IsLeapYear(CalendarType calendarType, int year)
        {
            bool isLeapYear;

            if (calendarType == CalendarType.Jalali)
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                isLeapYear = persianCalendar.IsLeapYear(year);
            }
            else
            {
                var gregorianCalendar = new GregorianCalendar();
                isLeapYear = gregorianCalendar.IsLeapYear(year);
            }

            return isLeapYear;
        }

        public DateTime CalculateToDateRange(DateTime dt, int RangeOrder, int RangeFromMonth, int RangeFromDay, int RangeToMonth, int RangeToDay, int culture)
        {
            try
            {
                Calendar calendar = null;
                switch (culture)
                {
                    case 1: calendar = new PersianCalendar(); break;
                    default: calendar = new GregorianCalendar(); break;
                }

                if (RangeOrder == 12 && RangeFromMonth > RangeToMonth)
                {
                    //در آخرین بازه محدوده، ماه پایان در سال بعد قرار گرفته
                    dt = calendar.AddYears(dt, 1);
                }
                else if (RangeOrder == 0 && RangeFromMonth >= RangeToMonth)
                {
                    //بازه سالانه است و پایان در سال بعد قرار گرفته
                    dt = calendar.AddYears(dt, 1);
                }

                if (calendar is PersianCalendar)
                {
                    if (calendar.IsLeapYear(calendar.GetYear(dt)) && RangeToMonth == 12 && RangeToDay == 29)
                    {
                        return calendar.ToDateTime(calendar.GetYear(dt), RangeToMonth, 30, 0, 0, 0, 0);
                    }
                    else
                    {
                        return calendar.ToDateTime(calendar.GetYear(dt), RangeToMonth, RangeToDay, 0, 0, 0, 0);
                    }
                }
                else
                {
                    if (calendar.IsLeapYear(dt.Year) && RangeToMonth == 2 && RangeToDay == 28)
                    {
                        //اگر سال کبسه بود و برای ماه فوریه روز 28 انتخاب شده بود
                        //به صورت خودکار با روز 29 جایگزین می شود
                        return calendar.ToDateTime(dt.Year, RangeToMonth, 29, 0, 0, 0, 0);
                    }
                    else
                    {
                        return calendar.ToDateTime(dt.Year, RangeToMonth, RangeToDay, 0, 0, 0, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DateTime CalculateFromDateRange(DateTime dt, int RangeOrder, int RangeFromMonth, int RangeFromDay, int RangeToMonth, int RangeToDay, int culture)
        {
            try
            {
                Calendar calendar = null;
                switch (culture)
                {
                    case 1: calendar = new PersianCalendar(); break;
                    default: calendar = new GregorianCalendar(); break;
                }
                if (RangeOrder == 1 && RangeFromMonth > RangeToMonth)
                {
                    //در اولین بازه محدوده، ماه شروع در سال قبل قرار گرفته
                    dt = calendar.AddYears(dt, -1);
                }
                else if (RangeOrder == 0 && RangeFromMonth >= RangeToMonth)
                {
                    //بازه سالانه است و شروع در سال قبل قرار گرفته
                    dt = calendar.AddYears(dt, -1);
                }

                return calendar.ToDateTime(calendar.GetYear(dt), RangeFromMonth, RangeFromDay, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DateTime> GetRangeBetweenTwoDate(DateTime startDate, DateTime endDate)
        {
            return Enumerable.Range(0, (endDate - startDate).Days + 1).Select(d => startDate.AddDays(d));
        }

    }
}
