namespace Eshop.Common.Helpers.AppSetting.Models
{
    public class OrderSetting : BaseSettingModel<OrderSetting>
    {
        public OrderSetting()
        {

        }
        public string OrderNumberCounterStart { get; set; }

    }
}

