using Eshop.Entity.General;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Entity.Identities
{
    public class RoleClaimEntity : IdentityRoleClaim<Guid>, IBaseEntity
    {
        #region BaseTrackedModel
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public Guid CreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        #endregion

        public virtual RoleEntity Role { get; set; }
        public string? RoutePath { get; set; }

    }
}