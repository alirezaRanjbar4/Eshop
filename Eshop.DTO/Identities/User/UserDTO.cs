using Eshop.DTO.General;
using System.ComponentModel.DataAnnotations;

namespace Eshop.DTO.Identities.User
{
    public class UserSimpleDTO
    {
        // basic user props
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? PersonCode { get; set; }

        public string? CardNumber { get; set; }

        public string? NationalCode { get; set; }

        public string? MobileNumber { get; set; }



        /// <summary>
        /// شناسه تننت
        /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// آبجکت تننت
        /// </summary>
    }

    public class UserDTO : UserSimpleDTO
    {

        public ICollection<UserRoleDTO> UserRoles { get; set; }

        // jwt token
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public ICollection<string> Claims { get; internal set; }
    }

    public class UserSearchDTO : BaseSearchDTO
    {
        public List<UserDTO> Users { get; set; } = new List<UserDTO>();
    }

    public class UserSearchInput
    {
        public string keyword { get; set; } = "";
        public Guid? roleid { get; set; }
        public int? page { get; set; }
        public int? pagesize { get; set; }
        public string sort { get; set; }
        public bool desc { get; set; }
    }

    public class UserRoleDTO : BaseDTO
    {

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }
        public string RoleNormalizedName { get; set; }
        public IList<RoleClaimDto> RoleClaims { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    public class RoleClaimDto
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }

    public class ContactPropertyDTO
    {
        public Guid? Id { get; set; }
        public Guid ContactId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
    #region Account Management


    public class ConfirmInput
    {
        public Guid UserId { get; set; }
        public string EmailToken { get; set; }
        public string PasswordToken { get; set; }
        public string Password { get; set; }
    }

    public class PasswordChangeInput
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
    }

    public class Login
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string? IPAddress { get; set; }
        public bool UpdateLogInCount { get; set; } = false;
    }

    public class ResetPassword
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
    #endregion
}

