using Eshop.Common.Enum;
using Eshop.Common.Helpers.Utilities.Interface;
using System;

namespace Eshop.Common.Helpers.Utilities.Utilities.Providers
{
    public class SystemSettingsHelper
    {
        private ICurrentUserProvider _currentUser;
        public SystemSettingsHelper(ICurrentUserProvider currentUser)
        {
            _currentUser = currentUser;
        }

        public UserCulture GetCurrentUserCulture()
        {
            return _currentUser.Culture;
        }

        public Guid GetCurrentUserId()
        {
            return _currentUser.UserId;
        }

        public Guid GetCurrentTenatId()
        {
            return _currentUser.TenantId;
        }

        public string GetCurrentUserName()
        {
            return _currentUser.Username.Trim();
        }

    }
}
