using Eshop.Share.Helpers.AppSetting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Presentation.Extensions.IOC
{
    public static class AppSettingConfiguration
    {
        public static void AppSetting(this IServiceCollection builder, IConfiguration configuration)
        {
            AppSettingHelper appSettingHelper = AppSettingHelper.Instance(configuration);
            appSettingHelper.Configuration = configuration;
        }
    }
}
