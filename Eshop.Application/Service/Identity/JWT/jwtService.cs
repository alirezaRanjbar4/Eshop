using Eshop.Application.DTO.Identities.DynamicAccess;
using Eshop.Application.DTO.Identities.User;
using Eshop.Domain.Identities;
using Eshop.Share.Enum;
using Eshop.Share.Helpers.AppSetting.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eshop.Application.Service.Identity.JWT
{
    public class JWTService : IJWTService
    {
        protected UserManager<UserEntity> _userManager;
        protected RoleManager<RoleEntity> _roleManager;
        private AppSettingData _appSettingData;
        public JWTService(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManage)
        {
            _userManager = userManager;
            _roleManager = roleManage;
            _appSettingData = new AppSettingData();
        }

        public async Task<string> GenerateTokenAsync(UserEntity user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_appSettingData.JWTSettings.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encrytionKey = Encoding.UTF8.GetBytes(_appSettingData.JWTSettings.EncryptKey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encrytionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var now = DateTime.Now;

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _appSettingData.JWTSettings.Issuer,
                Audience = _appSettingData.JWTSettings.Audience,
                IssuedAt = now,
                NotBefore = now,
                Expires = now.AddMinutes(Convert.ToDouble(_appSettingData.JWTSettings.ExpirationMinutes)),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(await GetClaimsAsync(user)),
                EncryptingCredentials = encryptingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(UserEntity user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("StoreId",user.Vendor!=null ? user.Vendor.StoreId.ToString() : string.Empty),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(new ClaimsIdentityOptions().SecurityStampClaimType,user.SecurityStamp),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            List<Claim> allUserClaims = new List<Claim>();


            var roles = user.UserRoles.Where(x => x.UserId == user.Id).ToList();
            if (roles != null && roles.Count > 0)
            {
                for (int i = 0; i < roles.Count; i++)
                {
                    var role = roles.ElementAt(i).Role;
                    var userClaims = await _roleManager.GetClaimsAsync(role);
                    if (userClaims != null && userClaims.Count > 0)
                    {
                        allUserClaims.AddRange(userClaims);
                    }
                }
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var item in allUserClaims)
                Claims.Add(new Claim(ConstantPolicies.DynamicPermissionClaimType, item.Value));

            foreach (var item in userRoles)
                Claims.Add(new Claim(ClaimTypes.Role, item));

            return Claims;
        }

        private IEnumerable<Claim> GetClaims(GetUserDto user)
        {
            return new List<Claim>()
            {
               new Claim(ClaimTypes.Name,user.UserName),
               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
               new Claim(typeof(CultureInfo).Name,UserCulture.Fa.ToString())
            };
        }

        public string GetClaimJWT(string tokenString, string claimType)
        {
            var secretKey = Encoding.UTF8.GetBytes(_appSettingData.JWTSettings.SecretKey);
            var encrytionKey = Encoding.UTF8.GetBytes(_appSettingData.JWTSettings.EncryptKey);


            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                TokenDecryptionKey = new SymmetricSecurityKey(encrytionKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidIssuer = _appSettingData.JWTSettings.Issuer,
                ValidAudience = _appSettingData.JWTSettings.Audience,
                ClockSkew = TimeSpan.FromMinutes(5)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(tokenString, tokenValidationParameters, out validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var claimValue = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
                return claimValue;
            }
            catch (Exception ex)
            {

                throw new Exception("Error reading token.", ex);
            }
        }

        public string GetClaimsDecryptJWT(string token, string claimTypes)
        {
            var handler = new JwtSecurityTokenHandler();

            var authHeader = token.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(token);
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var claimName = jwtToken.Claims.First(claim => claim.Type == claimTypes).Value;
            return claimName.ToString();
        }

        public RequestCurrentUserDto? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var secretKey = Encoding.UTF8.GetBytes(_appSettingData.JWTSettings.SecretKey);
            var encrytionKey = Encoding.UTF8.GetBytes(_appSettingData.JWTSettings.EncryptKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    TokenDecryptionKey = new SymmetricSecurityKey(encrytionKey),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _appSettingData.JWTSettings.Issuer,
                    ValidAudience = _appSettingData.JWTSettings.Audience,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };

                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name").Value;
                var userId = Guid.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid").Value);


                return new RequestCurrentUserDto { UserId = userId, UserName = userName };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var appsetting = _appSettingData.JWTSettings;
            return GenerateToken(_appSettingData.JWTSettings.RefreshTokenSecret, _appSettingData.JWTSettings.Issuer, _appSettingData.JWTSettings.Audience, Convert.ToDouble(_appSettingData.JWTSettings.RefreshTokenExpirationMinutes));
        }

        public string GenerateAccessToken(GetUserDto user)
        {
            return GenerateToken(_appSettingData.JWTSettings.AccessTokenSecret, _appSettingData.JWTSettings.Issuer, _appSettingData.JWTSettings.Audience, Convert.ToDouble(_appSettingData.JWTSettings.AccessTokenExpirationMinutes), GetClaims(user));
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettingData.JWTSettings.RefreshTokenSecret)),
                ValidIssuer = _appSettingData.JWTSettings.Issuer,
                ValidAudience = _appSettingData.JWTSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            try
            {
                jwtSecurityTokenHandler.ValidateToken(refreshToken, validationParameters,
                    out SecurityToken _);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GenerateToken(string secretKey, string issuer, string audience, double expires, IEnumerable<Claim> claims = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new(issuer, audience,
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(expires),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
