using Eshop.Presentation.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Linq;

namespace Eshop.Presentation.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseMiddlewares(this WebApplication app, IWebHostEnvironment env)
        {
            var cachePeriod = env.IsDevelopment() ? "600" : "604800";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sweet System V1");
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("fa-IR"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fa-IR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                ApplyCurrentCultureToResponseHeaders = true
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
