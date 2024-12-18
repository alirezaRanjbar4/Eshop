using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;


namespace Eshop.Api.Components
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userId = _contextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                return userId != null ? Guid.Parse(userId.Value) : Guid.Empty;
            }
        }

        public string Username
        {
            get
            {
                var username = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                return username.Value != null ? username.Value : "";
            }
        }

        public Guid TenantId
        {
            get
            {
                var tenantId = _contextAccessor.HttpContext.User.FindFirst("TenantId");
                return tenantId != null ? new Guid(tenantId.Value) : Guid.Empty;
            }
        }

        public UserCulture Culture
        {
            get
            {
                var userCulture = _contextAccessor.HttpContext.User.FindFirst(typeof(CultureInfo).Name);
                return userCulture != null ? (UserCulture)System.Enum.Parse(typeof(UserCulture), userCulture.Value) : UserCulture.Fa;
            }
        }

        public string UserIP
        {
            get
            {
                return _contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }
        }
    }
}
