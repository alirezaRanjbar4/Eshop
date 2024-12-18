using System.Security.Principal;

namespace Eshop.Common.Helpers.AppSetting.Models
{
    public class AppInfoSetting : BaseSettingModel<AppInfoSetting>
    {
        public AppInfoSetting()
        {

        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Favicon { get; set; }
        public string MetaDescriptionTag { get; set; }
        public string Url { get; set; }
        public string FileStorageUrl { get; set; }
    }
}

