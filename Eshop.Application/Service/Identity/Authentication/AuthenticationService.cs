﻿using AutoMapper;
using Eshop.Application.DTO.Identities.Authentication;
using Eshop.Application.DTO.Identities.User;
using Eshop.Application.Service.Identity.JWT;
using Eshop.Domain.Identities;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Enum;
using Eshop.Share.Exceptions;
using Eshop.Share.Helpers.Utilities.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Claims;


namespace Eshop.Application.Service.Identity.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Constructor        

        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private IMapper _mapper;
        private readonly IJWTService _jwtService;


        public AuthenticationService(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IMapper mapper,
            IJWTService jwtService
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        #endregion

        public async Task<LoginDTO> Login(Login model, HttpContext httpContext, CancellationToken cancellationToken)
        {
            try
            {
                UiValidationException validationExceptions = new UiValidationException(ResultType.Error);

                if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                    model.IPAddress = httpContext.Request.Headers["X-Forwarded-For"];
                else
                    model.IPAddress = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (user.WrongPasswordCount >= 5)
                    {
                        user.Activated = false;
                        await _userManager.UpdateAsync(user);
                    }

                    if (!user.Activated)
                    {
                        validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.UserNotActive);
                        throw validationExceptions;
                    }

                    if (!model.UpdateLogInCount)
                    {
                        if (user.WrongPasswordCount >= 3)
                        {
                            return new LoginDTO()
                            {
                                NeedCaptcha = true,
                            };
                        }
                    }

                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        user.WrongPasswordCount = 0;

                        var userPerson = await _userManager.Users
                                                           .Where(o => o.Id == user.Id)
                                                           .Include(x => x.UserRoles.Where(x => !x.Deleted)).ThenInclude(x => x.Role).ThenInclude(x => x.RoleClaims.Where(x => !x.Deleted))
                                                           .Include(x => x.Vendor).ThenInclude(x => x.Store)
                                                           .Include(x => x.AccountParty)
                                                           .FirstOrDefaultAsync(cancellationToken);

                        if (userPerson != null && userPerson.UserRoles != null && userPerson.UserRoles.Count > 0)
                        {
                            var dto = _mapper.Map<LoginDTO>(userPerson);
                            var claimsJson = JsonConvert.SerializeObject(dto.Claims);
                            dto.SecureClaims = Utility.SecurityHelper.CompressString(claimsJson);
                            dto.Claims = null;
                            dto.Token = await _jwtService.GenerateTokenAsync(user);
                            dto.RefreshToken = await _jwtService.GenerateRefreshTokenAsync(user, model.IPAddress);
                            user.LastLoginOn = DateTime.UtcNow;
                            user.Deleted = false;
                            await _userManager.UpdateAsync(user);

                            return dto;
                        }

                        validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.RoleNotExist);
                        throw validationExceptions;
                    }
                    else
                    {
                        user.WrongPasswordCount++;
                        await _userManager.UpdateAsync(user);
                        validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.UserNameOrPasswordIsIncorrect);
                        throw validationExceptions;
                    }
                }
                else
                {
                    validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.UserNameOrPasswordIsIncorrect);
                    throw validationExceptions;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<LoginDTO?> RefreshToken(TokenRequestDTO request, CancellationToken cancellationToken)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
                return null;

            var userId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return null;

            var user = await _userManager.Users
                .Include(x => x.UserRoles.Where(x => !x.Deleted)).ThenInclude(x => x.Role).ThenInclude(x => x.RoleClaims.Where(x => !x.Deleted))
                .Include(x => x.Vendor).ThenInclude(x => x.Store)
                .Include(x => x.AccountParty)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId), cancellationToken);

            if (user == null)
                return null;

            var refreshTokenEntity = await _jwtService.GetRefreshToken(request.RefreshToken);
            if (refreshTokenEntity == null || refreshTokenEntity.UserId != user.Id || refreshTokenEntity.ExpiresAt < DateTime.UtcNow)
                return null;

            var dto = _mapper.Map<LoginDTO>(user);
            var claimsJson = JsonConvert.SerializeObject(dto.Claims);
            dto.SecureClaims = Utility.SecurityHelper.CompressString(claimsJson);
            dto.Claims = null;
            dto.Token = await _jwtService.GenerateTokenAsync(user);
            dto.RefreshToken = await _jwtService.GenerateRefreshTokenAsync(user, refreshTokenEntity.CreatedByIp);

            return dto;
        }

        public async Task<OperationResult<string>> Logout(HttpContext httpContext)
        {
            var jsonToken = httpContext.Request.Headers["Authorization"];
            await _signInManager.SignOutAsync();
            return new OperationResult<string>
            {
                Data = "Collection Remove"
            };
        }

        public async Task<IdentityResult> ResetPassword(ResetPassword resetPassword)
        {
            UiValidationException validationExceptions = new UiValidationException(ResultType.Error);
            if (string.IsNullOrEmpty(resetPassword.CurrentPassword) || string.IsNullOrEmpty(resetPassword.NewPassword))
            {
                validationExceptions.OperationState.ResourceKeyList.Add(AuthenticationResourceEnums.PasswordCannotEmpty);
                throw validationExceptions;
            }
            var user = await _userManager.FindByIdAsync(resetPassword.UserId.ToString());
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "UserId Not Found", Code = "500" });

            return await _userManager.ChangePasswordAsync(user, resetPassword.CurrentPassword, resetPassword.NewPassword);
        }
    }
}
