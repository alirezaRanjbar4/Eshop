using Eshop.Application.DTO.Identities.User;
using Eshop.Domain.Identities;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Application.Service.Identity.JWT
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
