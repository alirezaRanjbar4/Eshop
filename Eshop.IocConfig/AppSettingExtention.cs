using Eshop.Common.Helpers.AppSetting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.IocConfig
{
    public static class AppSettingExtention
    {
        public static void AppSetting(this IServiceCollection builder, IConfiguration configuration)
        {
            AppSettingHelper appSettingHelper = AppSettingHelper.Instance(configuration);
            appSettingHelper.Configuration = configuration;
        }
    }
}
