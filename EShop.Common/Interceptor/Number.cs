using System.Data;
using System.Data.Common;

namespace Eshop.Common.Interceptor;

/// <summary>
///  کلاس مبدل اعداد فارسی به انگلیسی 
/// </summary>
public static class Number
{
    /// <summary>
    /// کد انگلیسی 
    /// </summary>
    public const char English0Char = (char)0048;
    public const char English1Char = (char)0049;
    public const char English2Char = (char)0050;
    public const char English3Char = (char)0051;
    public const char English4Char = (char)0052;
    public const char English5Char = (char)0053;
    public const char English6Char = (char)0054;
    public const char English7Char = (char)0055;
    public const char English8Char = (char)0056;
    public const char English9Char = (char)0057;


    /// <summary>
    /// کد فارسی  
    /// </summary>
    public const char Persian0Char = (char)1776;
    public const char Persian1Char = (char)1777;
    public const char Persian2Char = (char)1778;
    public const char Persian3Char = (char)1779;
    public const char Persian4Char = (char)1780;
    public const char Persian5Char = (char)1781;
    public const char Persian6Char = (char)1782;
    public const char Persian7Char = (char)1783;
    public const char Persian8Char = (char)1784;
    public const char Persian9Char = (char)1785;



    /// <summary>
    /// تبدیل کد فارسی به انگلیسی
    /// </summary>
    /// <param name="data">آبجکت ورودی جهت اصلاح</param>
    /// <returns>آبجکت اصلاح شده</returns>
    public static string? ApplyCorrectNumber(this object? data)
    {
        return data?.ToString().ApplyCorrectNumber();
    }

    /// <summary>
    /// تبدیل کد کد فارسی به انگلیسی 
    /// با استفاده از دستور Trim
    /// </summary>
    /// <param name="data">رشته ورودی جهت اصلاح</param>
    /// <returns>رشته اصلاح شده</returns>
    public static string? ApplyCorrectNumber(this string? data)
    {
        return string.IsNullOrWhiteSpace(data) ?
                    string.Empty :
                     data.Replace(Persian0Char, English0Char).Replace(Persian1Char, English1Char)
                    .Replace(Persian2Char, English2Char).Replace(Persian3Char, English3Char)
                    .Replace(Persian4Char, English4Char).Replace(Persian5Char, English5Char)
                    .Replace(Persian6Char, English6Char).Replace(Persian7Char, English7Char)
                    .Replace(Persian8Char, English8Char).Replace(Persian9Char, English9Char).Trim();
    }

    /// <summary>
    /// تبدیل کد فارسی به انگلیسی 
    /// بدون استفاده از دستور Trim
    /// </summary>
    /// <param name="data">رشته ورودی جهت اصلاح</param>
    /// <returns>رشته اصلاح شده</returns>
    public static string ApplyCorrectNumberWitOutTrim(this string data)
    {
        return string.IsNullOrWhiteSpace(data) ?
                    string.Empty :
                    data.Replace(Persian0Char, English0Char).Replace(Persian1Char, English1Char)
                    .Replace(Persian2Char, English2Char).Replace(Persian3Char, English3Char)
                    .Replace(Persian4Char, English4Char).Replace(Persian5Char, English5Char)
                    .Replace(Persian6Char, English6Char).Replace(Persian7Char, English7Char)
                    .Replace(Persian8Char, English8Char).Replace(Persian9Char, English9Char);
    }

    /// <summary>
    /// تبدیل کد فارسی به انگلیسی 
    /// </summary>
    /// <param name="command"> دستور ارسال شده به دیتابیس جهت اجرا</param>
    public static void ApplyCorrectNumber(this DbCommand command)
    {
        command.CommandText = command.CommandText.ApplyCorrectNumber();

        foreach (DbParameter parameter in command.Parameters)
        {
            switch (parameter.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    parameter.Value = parameter.Value.ApplyCorrectNumber();
                    break;
            }
        }
    }

}
