using System.ComponentModel;

namespace Eshop.Enum
{
    public enum StorType
    {
        [Description("تایپ1")]
        Type1 = 1,

        [Description("تایپ2")]
        Type2 = 2,

        [Description("تایپ3")]
        Type3 = 3,
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
}
