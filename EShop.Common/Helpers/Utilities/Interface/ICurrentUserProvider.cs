using Eshop.Enum;
using System;

namespace Eshop.Common.Helpers.Utilities.Interface
{
    public interface ICurrentUserProvider
    {
        public string Username { get; }
        public string UserIP { get; }
        public UserCulture Culture { get; }
        public Guid UserId { get; }
        public Guid TenantId { get; }
    }
}
