namespace Eshop.Share.Helpers.AppSetting.Models
{
    public class BaseSetting : BaseSettingModel<BaseSetting>
    {
        public BaseSetting()
        {
        }
        public string? EndpointAddress { get; set; }

    }
}
