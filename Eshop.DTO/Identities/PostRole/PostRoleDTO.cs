using Eshop.DTO.General;

namespace Eshop.DTO.Identities.PostRole
{
    public class PostRoleDTO : BaseDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public Guid PostId { get; set; }
        public Guid RoleId { get; set; }
    }
}
