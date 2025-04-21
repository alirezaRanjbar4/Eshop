using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.IocConfig
{
    public static class AddIdentityOptionConfiguration
    {
        public static IServiceCollection AddIdentityOption(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+اآبثجحخدذرزسشصضطظعغفقكلمنهويةءئإألآلأاآت ب ث پ ج ح چ خ دذرزس ش ص ض ط ظ ع غ ف ق ك ک ل م ن ه وي ةءئ إألآلأ";
            });

            return services;
        }
    }
}
