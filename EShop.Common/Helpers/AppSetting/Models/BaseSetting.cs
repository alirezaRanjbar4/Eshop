namespace Eshop.Common.Helpers.AppSetting.Models
{
    public class BaseSetting : BaseSettingModel<BaseSetting>
    {
        public BaseSetting()
        {
        }
        public string? EndpointAddress { get; set; }

    }
}
