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


        public string MiladiToShamsi(DateTime date, bool containsSecond)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                var shamsiYear = pc.GetYear(date);
                var shamsiMonth = pc.GetMonth(date);
                var shamsiDay = pc.GetDayOfMonth(date);

                // استخراج ساعت، دقیقه و ثانیه
                var hour = date.Hour;
                var minute = date.Minute;
                var second = date.Second;

                if (containsSecond)
                    return $"{hour.ToString("00")}:{minute.ToString("00")}:{second.ToString("00")} - {shamsiYear.ToString().PadLeft(4, '0')}/{shamsiMonth.ToString().PadLeft(2, '0')}/{shamsiDay.ToString().PadLeft(2, '0')}";
                else
                    return $"{hour.ToString("00")}:{minute.ToString("00")} - {shamsiYear.ToString().PadLeft(4, '0')}/{shamsiMonth.ToString().PadLeft(2, '0')}/{shamsiDay.ToString().PadLeft(2, '0')}";
            }
            catch (Exception e)
            {
                throw new Exception($"Sended date: {date} {e.Message}");
            }
        }

        public string UTCToShamsiWithIranTime(DateTime utcDate, bool containsSecond)
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

    }
}
