using System.ComponentModel;

namespace Eshop.Enum
{
    public enum StoreType
    {
        [Description("فروش محصول")]
        ProductSeller = 1,

        [Description("فروش خدمات")]
        ServiceSeller = 2,

        [Description("ترکیبی")]
        Mixed = 3,
    }

    public enum UserType
    {
        [Description("ادمین")]
        Admin = 1,

        [Description("فروشنده")]
        Vendor = 2,

        [Description("طرف حساب")]
        AccountParty = 3,
    }

    public enum MeasurementUnit
    {
        [Description("عدد")]
        Type1 = 1,

        [Description("بسته")]
        Type2 = 2,

        [Description("کارتن")]
        Type3 = 3,

        [Description("متر")]
        Type4 = 4,
    }

    public enum OrderStatus
    {
        [Description("در انتظار تایید")]
        Type1 = 1,

        [Description("تحویل داده شده")]
        Type2 = 2,

        [Description("رد شده")]
        Type3 = 3,
    }

    public enum ProductTransferType
    {
        [Description("افزایش")]
        Increase = 1,

        [Description("کاهش")]
        Decrease = 2
    }

    public enum CategoryType
    {
        [Description("کالا")]
        Product = 1,

        [Description("سرویس")]
        Service = 2
    }

    public enum AccountPartyType
    {
        [Description("مشتری")]
        Customer = 1,

        [Description("تامین کننده")]
        Supplier = 2
    }

    public enum ReceiptType
    {
        [Description("ورودی")]
        Enter = 1,

        [Description("خروجی")]
        Exit = 2
    }
}
