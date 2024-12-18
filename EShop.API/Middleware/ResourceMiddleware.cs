using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Eshop.Api.Middleware
{
    public class ResourceMiddleware
    {
        private readonly RequestDelegate _next;

        public ResourceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                // جلوگیری از بستن Stream اصلی تا بتوانیم بعداً دوباره از آن استفاده کنیم
                context.Response.Body = memoryStream;

                // اجرای میان‌افزار بعدی
                await _next(context);

                // Seek باید پس از خواندن محتوا و قبل از بازگرداندن به Stream اصلی باشد
                memoryStream.Seek(0, SeekOrigin.Begin);

                // خواندن محتوای Stream به رشته
                using (var reader = new StreamReader(memoryStream))
                {
                    var responseBody = await reader.ReadToEndAsync();

                    // اگر محتوا حاوی object با نام resoursekeyresult باشد
                    if (responseBody.Contains("resoursekeyresult"))
                    {
                        // تغییرات در محتوا انجام شود
                        responseBody = ModifyResourceKeyResult(responseBody);
                    }

                    // بازگرداندن محتوای تغییر یافته به Stream اصلی
                    using (var originalStreamWriter = new StreamWriter(originalBody))
                    {
                        await originalStreamWriter.WriteAsync(responseBody);
                        context.Response.Body = originalBody;
                    }
                }
            }
        }

        private string ModifyResourceKeyResult(string responseBody)
        {
            // اعمال تغییرات به محتوا
            // مثلاً در اینجا یک تغییر موقتی اعمال شده است
            return responseBody.Replace("ResourceKeyResult", "ModifiedValue");
        }
    }
}
