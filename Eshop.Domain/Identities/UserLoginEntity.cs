using Eshop.Domain.General;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Domain.Identities
{
    public class UserLoginEntity : IdentityUserLogin<Guid>, IBaseEntity
    {
        #region BaseTrackedModel
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public Guid CreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        #endregion

        public virtual UserEntity User { get; set; }
    }
}
