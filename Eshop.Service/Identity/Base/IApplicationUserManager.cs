using Eshop.Entity.Identities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Eshop.Service.Identity.Base
{
    public interface IApplicationUserManager
    {
        #region BaseClass
        IPasswordHasher<UserEntity> PasswordHasher { get; set; }
        IList<IUserValidator<UserEntity>> UserValidators { get; }
        IList<IPasswordValidator<UserEntity>> PasswordValidators { get; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        IdentityOptions Options { get; set; }
        bool SupportsUserAuthenticationTokens { get; }
        bool SupportsUserAuthenticatorKey { get; }
        bool SupportsUserTwoFactorRecoveryCodes { get; }
        bool SupportsUserTwoFactor { get; }
        bool SupportsUserPassword { get; }
        bool SupportsUserSecurityStamp { get; }
        bool SupportsUserRole { get; }
        bool SupportsUserLogin { get; }
        bool SupportsUserEmail { get; }
        bool SupportsUserPhoneNumber { get; }
        bool SupportsUserClaim { get; }
        bool SupportsUserLockout { get; }
        bool SupportsQueryableUsers { get; }
        IQueryable<UserEntity> Users { get; }
        Task<string> GenerateConcurrencyStampAsync(UserEntity user);
        Task<IdentityResult> CreateAsync(UserEntity user);
        Task<IdentityResult> UpdateAsync(UserEntity user);
        Task<IdentityResult> DeleteAsync(UserEntity user);
        Task<UserEntity> FindByIdAsync(string userId);
        Task<UserEntity> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(UserEntity user, string password);
        string NormalizeName(string name);
        string NormalizeEmail(string email);
        Task UpdateNormalizedUserNameAsync(UserEntity user);
        Task<string> GetUserNameAsync(UserEntity user);
        Task<IdentityResult> SetUserNameAsync(UserEntity user, string userName);
        Task<string> GetUserIdAsync(UserEntity user);
        Task<bool> CheckPasswordAsync(UserEntity user, string password);
        Task<bool> HasPasswordAsync(UserEntity user);
        Task<IdentityResult> AddPasswordAsync(UserEntity user, string password);
        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string currentPassword, string newPassword);
        Task<IdentityResult> RemovePasswordAsync(UserEntity user);
        Task<string> GetSecurityStampAsync(UserEntity user);
        Task<IdentityResult> UpdateSecurityStampAsync(UserEntity user);
        Task<string> GeneratePasswordResetTokenAsync(UserEntity user);
        Task<IdentityResult> ResetPasswordAsync(UserEntity user, string token, string newPassword);
        Task<UserEntity> FindByLoginAsync(string loginProvider, string providerKey);
        Task<IdentityResult> RemoveLoginAsync(UserEntity user, string loginProvider, string providerKey);
        Task<IdentityResult> AddLoginAsync(UserEntity user, UserLoginInfo login);
        Task<IList<UserLoginInfo>> GetLoginsAsync(UserEntity user);
        Task<IdentityResult> AddClaimAsync(UserEntity user, Claim claim);
        Task<IdentityResult> AddClaimsAsync(UserEntity user, IEnumerable<Claim> claims);
        Task<IdentityResult> ReplaceClaimAsync(UserEntity user, Claim claim, Claim newClaim);
        Task<IdentityResult> RemoveClaimAsync(UserEntity user, Claim claim);
        Task<IdentityResult> RemoveClaimsAsync(UserEntity user, IEnumerable<Claim> claims);
        Task<IList<Claim>> GetClaimsAsync(UserEntity user);
        Task<IdentityResult> AddToRoleAsync(UserEntity user, string role);
        Task<IdentityResult> AddToRolesAsync(UserEntity user, IEnumerable<string> roles);
        Task<IdentityResult> RemoveFromRoleAsync(UserEntity user, string role);
        Task<IdentityResult> RemoveFromRolesAsync(UserEntity user, IEnumerable<string> roles);
        Task<IList<string>> GetRolesAsync(UserEntity user);
        Task<bool> IsInRoleAsync(UserEntity user, string role);
        Task<string> GetEmailAsync(UserEntity user);
        Task<IdentityResult> SetEmailAsync(UserEntity user, string email);
        Task<UserEntity> FindByEmailAsync(string email);
        Task UpdateNormalizedEmailAsync(UserEntity user);
        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);
        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);
        Task<bool> IsEmailConfirmedAsync(UserEntity user);
        Task<string> GenerateChangeEmailTokenAsync(UserEntity user, string newEmail);
        Task<IdentityResult> ChangeEmailAsync(UserEntity user, string newEmail, string token);
        Task<string> GetPhoneNumberAsync(UserEntity user);
        Task<IdentityResult> SetPhoneNumberAsync(UserEntity user, string phoneNumber);
        Task<IdentityResult> ChangePhoneNumberAsync(UserEntity user, string phoneNumber, string token);
        Task<bool> IsPhoneNumberConfirmedAsync(UserEntity user);
        Task<string> GenerateChangePhoneNumberTokenAsync(UserEntity user, string phoneNumber);
        Task<bool> VerifyChangePhoneNumberTokenAsync(UserEntity user, string token, string phoneNumber);
        Task<bool> VerifyUserTokenAsync(UserEntity user, string tokenProvider, string purpose, string token);
        Task<string> GenerateUserTokenAsync(UserEntity user, string tokenProvider, string purpose);
        void RegisterTokenProvider(string providerName, IUserTwoFactorTokenProvider<UserEntity> provider);
        Task<IList<string>> GetValidTwoFactorProvidersAsync(UserEntity user);
        Task<bool> VerifyTwoFactorTokenAsync(UserEntity user, string tokenProvider, string token);
        Task<string> GenerateTwoFactorTokenAsync(UserEntity user, string tokenProvider);
        Task<bool> GetTwoFactorEnabledAsync(UserEntity user);
        Task<IdentityResult> SetTwoFactorEnabledAsync(UserEntity user, bool enabled);
        Task<bool> IsLockedOutAsync(UserEntity user);
        Task<IdentityResult> SetLockoutEnabledAsync(UserEntity user, bool enabled);
        Task<bool> GetLockoutEnabledAsync(UserEntity user);
        Task<DateTimeOffset?> GetLockoutEndDateAsync(UserEntity user);
        Task<IdentityResult> SetLockoutEndDateAsync(UserEntity user, DateTimeOffset? lockoutEnd);
        Task<IdentityResult> AccessFailedAsync(UserEntity user);
        Task<IdentityResult> ResetAccessFailedCountAsync(UserEntity user);
        Task<int> GetAccessFailedCountAsync(UserEntity user);
        Task<IList<UserEntity>> GetUsersForClaimAsync(Claim claim);
        Task<IList<UserEntity>> GetUsersInRoleAsync(string roleName);
        Task<string> GetAuthenticationTokenAsync(UserEntity user, string loginProvider, string tokenName);
        Task<IdentityResult> SetAuthenticationTokenAsync(UserEntity user, string loginProvider, string tokenName, string tokenValue);
        Task<IdentityResult> RemoveAuthenticationTokenAsync(UserEntity user, string loginProvider, string tokenName);
        Task<string> GetAuthenticatorKeyAsync(UserEntity user);
        Task<IdentityResult> ResetAuthenticatorKeyAsync(UserEntity user);
        string GenerateNewAuthenticatorKey();
        Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(UserEntity user, int number);
        Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(UserEntity user, string code);
        Task<int> CountRecoveryCodesAsync(UserEntity user);
        Task<byte[]> CreateSecurityTokenAsync(UserEntity user);

        #endregion

        #region CustomMethod
        // Task<List<User>> GetAllUsersAsync();
        //  Task<List<UsersViewModel>> GetAllUsersWithRolesAsync();
        //Task<UsersViewModel> FindUserWithRolesByIdAsync(int UserId);
        //  Task<string> GetFullName(ClaimsPrincipal User);
        // Task<User> GetUserAsync(ClaimsPrincipal User);
        // Task<List<UsersViewModel>> GetPaginateUsersAsync(int offset, int limit, bool? firstnameSortAsc, bool? lastnameSortAsc, bool? emailSortAsc, bool? usernameSortAsc, bool? registerDateTimeSortAsc, string searchText);

        //Before Migration To NetCore 3 and Use Package System.linq.Dynamic.Core
        //List<UsersViewModel> GetPaginateUsers(int offset, int limit, Func<UsersViewModel, Object> orderByAscFunc, Func<UsersViewModel, Object> orderByDescFunc, string searchText);
        // Task<List<UsersViewModel>> GetPaginateUsersAsync(int offset, int limit, string orderBy, string searchText);
        // string CheckAvatarFileName(string fileName);
        //    Task<User> FindClaimsInUser(int userId);
        Task<IdentityResult> AddOrUpdateClaimsAsync(Guid userId, string userClaimType, IList<string> selectedUserClaimValues);
        #endregion
    }
}
