using Eshop.Share.Enum;
using System;

namespace Eshop.Share.Helpers.Utilities.Interface
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
