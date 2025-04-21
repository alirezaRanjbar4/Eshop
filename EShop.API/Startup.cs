using Eshop.Api.Components;
using Eshop.Api.Middleware;
using Eshop.Common.ActionFilters;
using Eshop.Common.Enum;
using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.IocConfig;
using Eshop.Service.FileStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.IO;
using System.Linq;


namespace Eshop.Api
{
    public class Startup
    {
        public static IWebHostEnvironment AppEnvironment { get; private set; }
        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            AppEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
              .Build();

            services.AddApplicationInsightsTelemetry(Configuration);

            services.AppSetting(configuration);

            services.AddMemoryCache();

            services.AddHttpContextAccessor();

            services.AddSwagger();

            services.AddDbContext(configuration);

            // as of 3.1.1 the internal .net core JSON doens't handle referenceloophandling so we still need to use Newtonsoft
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddIdentityServiceConfiguration();

            services.AddCorss();

            // add appsettings availability
            services.AddSingleton(Configuration);

            // ability to grab httpcontext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IFileStorageService, FileStorageService>();

            services.AddScoped<UserActionFilter>();

            services.Injector();

            services.AddApiVersioning();

            services.AddAutomapperServiceConfiguration();

            services.AddDynamicPermission();

            services.AddMvc();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            services.AddLoggings(configuration);

            // configure jwt authentication
            services.AddCustomAuthentication();

            services.AddIdentityOption();

            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

            services.AddScoped<IUtility, Utility>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstantPolicies.DynamicPermission, policy => policy.Requirements.Add(new DynamicPermissionRequirement()));
            });

            services.AddLocalization();

            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties†.
                options.FormFieldName = "AntiforgeryField";
                options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
                options.SuppressXFrameOptionsHeader = false;
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 404857600; // 100 MB  300_000_000
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 404857600;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 404857600;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sweet System V1");
            });


            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IUtility>();
            }

            // use the following if hosting files/images using StandardFileService 
            var cachePeriod = env.IsDevelopment() ? "600" : "604800";

            // تنظیم فایل‌های استاتیک برای ClientApp (پروژه React)
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });

            var supportedCultures = new[]
            {
              new CultureInfo(LanguageType.en_US.ToString()),
              new CultureInfo(LanguageType.fa_IR.ToString()),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fa-IR"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures,
                ApplyCurrentCultureToResponseHeaders = true
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
