using Eshop.Entity.General;
using Eshop.Entity.Models;
using Eshop.Enum;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Entity.Identities
{
    public class UserEntity : IdentityUser<Guid>, IBaseEntity
    {
        #region BaseTrackedModel
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public bool Activated { get; set; }
        public Guid CreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        #endregion

        public int WrongPasswordCount { get; set; }
        public DateTime? LastLoginOn { get; set; }

        public UserType UserType { get; set; }

        public virtual VendorEntity? Vendor { get; set; }
        public virtual AccountPartyEntity? AccountParty { get; set; }

        public virtual ICollection<UserClaimEntity> Claims { get; set; }
        public virtual ICollection<UserLoginEntity> Logins { get; set; }
        public virtual ICollection<UserTokenEntity> Tokens { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
