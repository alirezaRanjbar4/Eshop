using System.Data;
using System.Data.Common;

namespace Eshop.Share.Interceptor;

/// <summary>
/// کلاس مبدل 'ی' و 'ک' عربی به فارسی
/// </summary>
public static class YeKe
{
    /// <summary>
    /// کد عربی ی
    /// </summary>
    public const char ArabicYeChar = (char)1610;

    /// <summary>
    /// کد فارسی ی
    /// </summary>
    public const char PersianYeChar = (char)1740;

    /// <summary>
    /// کد عربی ک
    /// </summary>
    public const char ArabicKeChar = (char)1603;

    /// <summary>
    /// کد فارسی ک
    /// </summary>
    public const char PersianKeChar = (char)1705;

    /// <summary>
    /// تبدیل کد عربی به فارسی
    /// </summary>
    /// <param name="data">آبجکت ورودی جهت اصلاح</param>
    /// <returns>آبجکت اصلاح شده</returns>
    public static string? ApplyCorrectYeKe(this object? data)
    {
        return data == null ? null : data.ToString().ApplyCorrectYeKe();
    }

    /// <summary>
    /// تبدیل کد عربی به فارسی 
    /// با استفاده از دستور Trim
    /// </summary>
    /// <param name="data">رشته ورودی جهت اصلاح</param>
    /// <returns>رشته اصلاح شده</returns>
    public static string? ApplyCorrectYeKe(this string data)
    {
        return string.IsNullOrWhiteSpace(data) ?
                    string.Empty :
                    data.Replace(ArabicYeChar, PersianYeChar).Replace(ArabicKeChar, PersianKeChar).Trim();
    }

    /// <summary>
    /// تبدیل کد عربی به فارسی 
    /// بدون استفاده از دستور Trim
    /// </summary>
    /// <param name="data">رشته ورودی جهت اصلاح</param>
    /// <returns>رشته اصلاح شده</returns>
    public static string ApplyCorrectYeKeWitOutTrim(this string data)
    {
        return string.IsNullOrWhiteSpace(data) ?
                    string.Empty :
                    data.Replace(ArabicYeChar, PersianYeChar).Replace(ArabicKeChar, PersianKeChar);
    }

    /// <summary>
    /// تبدیل کد عربی به فارسی 
    /// </summary>
    /// <param name="command"> دستور ارسال شده به دیتابیس جهت اجرا</param>
    public static void ApplyCorrectYeKe(this DbCommand command)
    {
        command.CommandText = command.CommandText.ApplyCorrectYeKe();

        foreach (DbParameter parameter in command.Parameters)
        {
            switch (parameter.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    parameter.Value = parameter.Value.ApplyCorrectYeKe();
                    break;
            }
        }
    }

}
