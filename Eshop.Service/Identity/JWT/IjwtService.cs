using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Identities.User;
using Eshop.Entity.Identities;

namespace Eshop.Service.Identity.JWT
{
    public interface IJWTService : IScopedDependency
    {
        Task<string> GenerateTokenAsync(UserEntity User);
        string GenerateRefreshToken();
        string GenerateAccessToken(GetUserDto user);
        bool ValidateRefreshToken(string refreshToken);
        string GetClaimsDecryptJWT(string token, string claimTypes);
        RequestCurrentUserDto? ValidateJwtToken(string token);
    }
}
