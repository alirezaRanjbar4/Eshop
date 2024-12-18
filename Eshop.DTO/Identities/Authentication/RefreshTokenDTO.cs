using Eshop.DTO.General;

namespace Eshop.DTO.Identities.Authentication
{
    public class RefreshTokenDTO : BaseDto
    {
        public string Token { get; set; }
        public DateTime Expire { get; set; }

        public string CreatedByIp { get; set; }

        public DateTime? Revoked { get; set; }

        public bool IsRevoked { get; set; }
        public bool IsActive { get; set; }
        public bool IsExpired { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// شناسه ویرایش کننده
        /// </summary>
        public Guid? ModifiedById { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Guid CreateById { get; set; }

        public Guid UserId { get; set; }
    }
}
