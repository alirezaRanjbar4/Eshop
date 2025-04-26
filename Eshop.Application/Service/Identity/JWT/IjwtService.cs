using Eshop.Application.DTO.Identities.User;
using Eshop.Domain.Identities;
using Eshop.Share.Helpers.Utilities.Interface;
using System.Security.Claims;

namespace Eshop.Application.Service.Identity.JWT
{
    public interface IJWTService : IScopedDependency
    {
        Task<string> GenerateTokenAsync(UserEntity User);
        Task<string> GenerateRefreshTokenAsync(UserEntity user, string ipAddress);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
        Task<RefreshTokenEntity?> GetRefreshToken(string refreshToken);


        string GenerateRefreshToken();
        string GenerateAccessToken(GetUserDto user);
        bool ValidateRefreshToken(string refreshToken);
        string GetClaimsDecryptJWT(string token, string claimTypes);
        RequestCurrentUserDto? ValidateJwtToken(string token);
    }
}
