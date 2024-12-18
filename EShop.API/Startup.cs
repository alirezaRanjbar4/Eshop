using Eshop.Api.Components;
using Eshop.Api.Middleware;
using Eshop.Common.ActionFilters;
using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.Enum;
using Eshop.IocConfig;
using Eshop.Service.FileStorage.Interface;
using Eshop.Service.FileStorage.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rasam.Data.DBContext;
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
            // Application Insights
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AppSetting(configuration);
            // enable caching
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Sweet System API",
                        Version = "v1",
                        Description = "API for handling user/content data",
                        // TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "keyvan Developer",
                            Email = "keyvan.alishiri@gmail.com",
                            Url = new Uri("https://webartdesign.ir"),
                        },
                    });

                var filePath = Path.Combine(AppContext.BaseDirectory, "RainstormTech.API.xml");
                c.IncludeXmlComments(filePath);
            });

            // define SQL Server connection string
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            );

            // as of 3.1.1 the internal .net core JSON doens't handle referenceloophandling so we still need to use Newtonsoft
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // add identity services
            services.AddIdentityServiceConfiguration();

            // enable CORS
            services.AddCors(options =>
                             options.AddPolicy("CorsPolicy", policy =>
                             {
                                 policy.AllowAnyHeader()
                                       .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                                       .WithOrigins("http://localhost:3000", "http://185.103.129.113", "http://192.168.3.17", "http://192.168.3.17:71", "http://192.168.3.12", "http://erp.rasamflexo.ir", "http://rasamflexo.ir", "http://192.168.5.182:3000", "http://192.168.5.134:3000")
                                       .AllowCredentials();
                             }));


            // add appsettings availability
            services.AddSingleton(Configuration);

            // ability to grab httpcontext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IFileStorageService, InAppStorageService>();
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

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();

            });

            // configure jwt authentication
            services.AddCustomAuthentication();

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

            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            services.AddScoped<IUtility, Utility>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstantPolicies.DynamicPermission, policy => policy.Requirements.Add(new DynamicPermissionRequirement()));
            });

            services.AddSignalR(option =>
            {
                option.EnableDetailedErrors = true;
                option.MaximumReceiveMessageSize = 5000000;
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
