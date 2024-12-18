using Eshop.Common.Exceptions;
using System;

namespace Eshop.Common.Helpers.Utilities.Utilities.Providers
{
    public class TimeConvertorHelper
    {
        static int MinutPerDay = 1440;
        static int MinutePerHour = 60;

        /// <summary>
        /// دقیقه را به زمان واقعی تبدیل میکند
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <example>
        /// IntTimeToRealTimeWithZero(130); --> 2:10
        /// IntTimeToRealTimeWithZero(1450); --> +0:10
        /// /// IntTimeToRealTimeWithZero(0); --> 00:00
        /// </example>/// 
        public string IntTimeToRealTimeWithZero(int Minute)
        {
            try
            {
                if (Minute == -1000) return "";
                int day = 0;
                string temp = "";
                if (Minute >= 0)
                {
                    day = Minute / MinutPerDay;
                    for (int i = 1; i <= day; i++)
                        temp += "+";
                    Minute -= MinutPerDay * day;
                }
                else if (Minute < 0)
                {
                    while (Minute < 0)
                    {
                        Minute += MinutPerDay;
                        temp += "-";
                    }
                }
                temp += (Minute / MinutePerHour).ToString().PadLeft(2, '0') + ":" + (Minute % MinutePerHour).ToString().PadLeft(2, '0');
                return temp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// .تبدیل دقیقه به زمان
        /// </summary>
        /// <param name="Minute">.میزان دقیقه ای که به زمان تبدیل می شود</param>
        /// <returns>زمان محاسبه شده</returns>
        /// <example>
        /// MinuteToTime(130); --> 2:10
        /// MinuteToTime(1450); --> 24:10
        /// </example>
        public string IntTimeToTime(int Minute)
        {
            try
            {
                if (Minute == -1000) return "     ";
                //if (Minute == 0) return "     ";
                bool negative = false;
                if (Minute < 0)
                {
                    Minute *= -1;
                    negative = true;
                }
                string time = (Minute / MinutePerHour).ToString().PadLeft(2, '0') + ":" + (Minute % MinutePerHour).ToString().PadLeft(2, '0');
                if (negative)
                {
                    time = "- " + time;
                }
                return time;
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, string.Format("Utility.IntTimeToTime({0})", Minute), ex);
            }
        }

        public string IntTimeToTime(decimal Minute)
        {
            try
            {
                return IntTimeToTime(Convert.ToInt32(Minute));
            }
            catch (Exception ex)
            {
                throw new BaseException(ex.Message, string.Format("Utility.IntTimeToTime({0})", Minute), ex);
            }
        }

        /// <summary>
        /// تبدیل زمان به دقیقه
        /// </summary>
        /// <param name="time">زمان</param>
        /// <returns>میزان دقیقه</returns>
        /// <example>
        /// TimeToMinute("2:10") --> 130
        /// TimeToMinute("+0:10") --> 1450
        /// </example>
        public int RealTimeToIntTime(string time)
        {
            try
            {
                if (string.IsNullOrEmpty(time)) return 0;
                int temp = 0;
                if (time.Contains(':'))
                {
                    switch (time[time.Length - 1])
                    {
                        case '+': temp += 1440; time = time.Remove(time.Length - 1, 1); break;
                        case '-': temp -= 1440; time = time.Remove(time.Length - 1, 1); break;
                    }
                    temp += Convert.ToInt32(time.Split(':')[0]) * 60 + Convert.ToInt32(time.Split(':')[1]);
                    return temp;
                }
                else
                {
                    throw new Exception("TimeFormatIsIncorrect");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string FixedDailyValue(int DailyValue)
        {
            if (DailyValue == -1000) return "";
            if (DailyValue == 0) return "";
            return DailyValue.ToString();
        }

        public string FixedDailyValue(decimal DailyValue)
        {
            if (DailyValue == -1000) return "";
            if (DailyValue == 0) return "";
            return DailyValue.ToString();
        }
    }
}
