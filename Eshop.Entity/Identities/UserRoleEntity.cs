using Eshop.Entity.General;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Entity.Identities
{
    public class UserRoleEntity : IdentityUserRole<Guid>, IBaseEntity
    {
        #region BaseTrackedModel
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public Guid CreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        #endregion

        public DateTime? StartDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}
