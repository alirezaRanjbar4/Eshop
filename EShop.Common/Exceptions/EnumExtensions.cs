using Eshop.Common.Helpers.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Eshop.Common.Exceptions
{
    public static class EnumExtensions
    {
        public static void Add(this List<ResourceKey> enumNotification, System.Enum notiicationEnums)
        {
            var enumType = notiicationEnums.GetType().Name;
            var enumValue = notiicationEnums.ConvertToString();
            var finndEnum = enumNotification.Find(x => x.EnumName == enumType);
            if (finndEnum != null)
            {
                finndEnum.ResourceKeys.Add(enumValue);
            }
            else
            {
                var test = notiicationEnums.ToString();
                var test2 = (object)notiicationEnums.ToString();
                enumNotification.Add(new ResourceKey(enumType, enumValue));
            }
        }

        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return System.Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static string ToDescription(this System.Enum value)
        {
            var da = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? da[0].Description : value.ToString();
        }

        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            foreach (var value in System.Enum.GetValues(input.GetType()))
                if ((input as System.Enum).HasFlag(value as System.Enum))
                    yield return (T)value;
        }

        public static string ConvertToString(this System.Enum eff)
        {
            return System.Enum.GetName(eff.GetType(), eff);
        }

        // تعریف تابع برای دریافت توضیحات
        public static string GetEnumDescription(this System.Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        // تعریف تابع برای تبدیل امن و دریافت توضیحات
        public static string GetEnumDescriptionSafe<TEnum>(string enumName) where TEnum : struct
        {
            if (System.Enum.TryParse(typeof(TEnum), enumName, out var enumValue) && enumValue is System.Enum enumInstance)
            {
                return enumInstance.GetEnumDescription();
            }
            else
            {
                return "Undefined"; // یا هر مقدار پیش‌فرض دیگری که می‌خواهید استفاده کنید
            }
        }

        public static string ToDisplay(this System.Enum value, DisplayProperty property = DisplayProperty.Name)
        {

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }
    }

    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}
