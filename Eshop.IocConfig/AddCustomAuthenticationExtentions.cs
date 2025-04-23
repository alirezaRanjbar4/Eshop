using Eshop.Application.Service.Identity.User;
using Eshop.Domain.Identities;
using Eshop.Share.Exceptions;
using Eshop.Share.Helpers.AppSetting.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Eshop.IocConfig
{
    public static class AddCustomAuthenticationExtentions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            AppSettingData appSettingData = new AppSettingData();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretkey = Encoding.UTF8.GetBytes(appSettingData.JWTSettings.SecretKey);
                var encryptionkey = Encoding.UTF8.GetBytes(appSettingData.JWTSettings.EncryptKey);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true,
                    ValidAudience = appSettingData.JWTSettings.Audience,

                    ValidateIssuer = true,
                    ValidIssuer = appSettingData.JWTSettings.Issuer,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception != null)
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Fail("TokenExpire.");
                            }
                        }
                        return Task.CompletedTask;

                    },

                    OnTokenValidated = async context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() != true)
                            context.Fail("This token has no claims.");

                        var securityStamp = claimsIdentity.FindFirst(new ClaimsIdentityOptions().SecurityStampClaimType).Value;
                        if (string.IsNullOrEmpty(securityStamp))
                            context.Fail("This token has no secuirty stamp");

                        Guid userId = Guid.Parse(claimsIdentity.FindFirst(new ClaimsIdentityOptions().UserIdClaimType).Value);
                        var user = await userService.GetAsync<UserEntity>(x => x.Id == userId, null, false, default);

                        if (user.SecurityStamp != securityStamp)
                            context.Fail("Token secuirty stamp is not valid.");

                        if (!user.Activated)
                            context.Fail("User is not active.");
                    },

                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                            // throw new SecurityTokenExpiredException(context.AuthenticateFailure.Message, context.AuthenticateFailure);
                            throw new UnAuthrizedException(HttpStatusCode.Unauthorized, "Authenticate failure.", context.AuthenticateFailure, null);
                        throw new AppException(HttpStatusCode.Unauthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                    }
                };
            });

            return services;
        }
    }
}