using Eshop.Domain.General;

namespace Eshop.Domain.Identities
{
    public class RefreshTokenEntity : BaseTrackedModel, IBaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
