using Eshop.Domain.Identities;
using System.ComponentModel.DataAnnotations;

namespace Eshop.Domain.General
{
    public class BaseTrackedModel : BaseModel
    {
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; } = false;

        public Guid CreateById { get; set; }
        public virtual UserEntity CreateBy { get; set; }

        public Guid? ModifiedById { get; set; }
        public virtual UserEntity? ModifiedBy { get; set; }
    }
}
