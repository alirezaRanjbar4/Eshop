using System.ComponentModel;

namespace Eshop.Share.Enum
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

    public enum FinancialDocumentType
    {
        [Description("دریافت")]
        Receive = 1,

        [Description("پرداخت")]
        Pay = 2
    }

    public enum FinancialDocumentPaymentMethod
    {
        [Description("دستگاه کارت خوان")]
        Pos = 1,

        [Description("نقد")]
        Cash = 2,

        [Description("جابه جایی بین بانکی")]
        BankTransfer = 3,

        [Description("چک بانکی")]
        Cheque = 4
    }
}
