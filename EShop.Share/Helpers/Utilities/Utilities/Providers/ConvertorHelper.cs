using Eshop.Share.Helpers.AppSetting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eshop.Share.Helpers.Utilities.Utilities.Providers
{
    public class ConvertorHelper
    {
        /// <summary>
        /// اگر مقدار اعشاری باشد تبدیل به عدد صحیح میکند
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ConvertValuePrarameter(string value)
        {
            if (!value.All(char.IsDigit))
            {
                var res = Convert.ToDecimal(value) * 100;
                var result = Convert.ToInt16(res - 100);
                return result.ToString();
            }
            return value;
        }

        public double ToDouble(object obj)
        {


            return Convert.ToDouble(obj);



        }

        public byte[]? ToUtf8EncodingByte(string? str) => Encoding.UTF8.GetBytes(str);

        public Dictionary<string, string?> ToDictionary<T>(List<T> listObject) where T : IProperty
        {
            return listObject.ToDictionary(item => item.PropertyName, item => item.Value);
        }

        public int ToInt32(object obj)
        {
            return Convert.ToInt32((int)obj);
        }

        public long ToInt64(object obj)
        {
            return Convert.ToInt64((int)obj);
        }

        public static string ConvertToBase64String(byte[] image)
        {
            string strImage = Convert.ToBase64String(image);
            if (!string.IsNullOrEmpty(strImage) && strImage != "null")
                strImage = "data:image/jpeg;base64," + strImage;
            else
                strImage = null;
            return strImage;
        }

        public static byte[] ConvertFromBase64String(string image)
        {
            if (!string.IsNullOrEmpty(image) && image != "null")
            {
                image = image.Replace("data:image/jpeg;base64,", "");

                byte[] imageBytes = Convert.FromBase64String(image);
                return imageBytes;
            }

            return new byte[0];
        }

        #region PersianStringNumber

        private static readonly string[] Ones = {
        "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه",
        "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده"
        };

        private static readonly string[] Tens = {
        "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود"
        };

        private static readonly string[] Hundreds = {
        "", "صد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد"
        };

        private static readonly Dictionary<long, string> PowersOfThousand = new Dictionary<long, string>
        {
          { 1000000000000, "تریلیون" },
          { 1000000000, "میلیارد" },
          { 1000000, "میلیون" },
          { 1000, "هزار" },
          // اضافه کردن واحدهای بزرگتر در صورت نیاز
        };

        public string ConvertToPersianWord(long number)
        {
            if (number == 0)
                return Ones[0];

            return ConvertToWords(number);
        }
        public float ConvertToFloat(string value)
        {
            float result;
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            // تغییر جداکننده اعشار از "٫" یا "," یا "/" به "."
            value = value.Replace('٫', '.').Replace(',', '.').Replace('/', '.');

            if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            return 0;
        }


        public decimal ConvertToDecimal(string value)
        {
            decimal result;
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            // تغییر جداکننده اعشار از "٫" یا "," یا "/" به "."
            value = value.Replace('٫', '.').Replace(',', '.').Replace('/', '.');

            if (decimal.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            return 0;
        }


        private string ConvertToWords(long number)
        {
            if (number < 0)
                return "منفی " + ConvertToWords(Math.Abs(number));

            string words = "";

            if (number < 20)
            {
                words = Ones[number];
            }
            else if (number < 100)
            {
                words = Tens[number / 10];
                if (number % 10 > 0)
                    words += " و " + Ones[number % 10];
            }
            else if (number < 1000)
            {
                words = Hundreds[number / 100];
                if (number % 100 > 0)
                    words += " و " + ConvertToWords(number % 100);
            }
            else
            {
                foreach (var power in PowersOfThousand)
                {
                    if (number >= power.Key)
                    {
                        long baseValue = power.Key;
                        long numBaseUnits = number / baseValue;
                        long remainder = number % baseValue;

                        words = ConvertToWords(numBaseUnits) + " " + power.Value;
                        if (remainder > 0)
                            words += " و " + ConvertToWords(remainder);

                        break;
                    }
                }
            }

            return words;
        }

        #endregion
    }
}
