using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NextToMe.Common;
using NextToMe.Common.DTOs;
using NextToMe.Common.Exceptions;
using NextToMe.Database;
using NextToMe.Database.Entities;
using NextToMe.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NextToMe.Common.Models;

namespace NextToMe.Services
{
    public class AuthService : IAuthService
    {
        private const string _emailConfirmationSubject = "NextToMe: Email confirmation";
        private const string _emailResetSubject = "NextToMe: Password Change";

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AppSettings _settings;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(
            ApplicationDbContext dbContext,
            UserManager<User> userManager,
            ILogger<AuthService> logger,
            IMapper mapper,
            IOptions<AppSettings> settings,
            IEmailService emailService,
            IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _settings = settings.Value;
            _userManager = userManager;
            _emailService = emailService;
            _contextAccessor = contextAccessor;
        }

        public async Task Register(RegisterRequest request)
        {
            User user = await _userManager.FindByEmailAsync(request.Login);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    throw new BadRequestException("Login is already taken");
                }
            }
            else
            {
                user = new User { Email = request.Login, UserName = request.Login };
                IdentityResult result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    throw new AuthException(result.Errors);
                }
                await _userManager.AddToRoleAsync(user, Roles.User);
            }

            var queryParams = new Dictionary<string, string>
            {
                { "userId", user.Id.ToString() },
                { "code", await _userManager.GenerateEmailConfirmationTokenAsync(user) }
            };
            string redirectUrl = QueryHelpers.AddQueryString(request.RedirectUrl, queryParams);

            string messageBody = _emailService.GetInvitationMessage(redirectUrl);
            await _emailService.SendEmailAsync(request.Login, _emailConfirmationSubject, messageBody);
        }

        public async Task ConfirmEmail(EmailConfirmRequest request)
        {
            if (request?.Code == null)
            {
                throw new AuthException("No code provided");
            }

            IdentityResult passwordValidationResult = await ValidatePassword(request.Password);
            if (!passwordValidationResult.Succeeded)
            {
                throw new AuthException(passwordValidationResult.Errors);
            }

            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
            {
                throw new AuthException("Incorrect user ID ");
            }

            bool isEmailTokenValid = await _userManager.VerifyUserTokenAsync(
                user,
                _userManager.Options.Tokens.EmailConfirmationTokenProvider,
                UserManager<User>.ConfirmEmailTokenPurpose,
                request.Code);

            if (!isEmailTokenValid)
            {
                throw new AuthException("Incorrect code", $"Email token is not valid for user {user}");
            }

            await _userManager.RemovePasswordAsync(user);
            IdentityResult passwordResult = await _userManager.AddPasswordAsync(user, request.Password);
            if (!passwordResult.Succeeded)
            {
                throw new AuthException(passwordResult.Errors);
            }

            user.EmailConfirmed = true;
            IdentityResult confirmEmailResult = await _userManager.UpdateAsync(user);
            if (!confirmEmailResult.Succeeded)
            {
                throw new AuthException(confirmEmailResult.Errors);
            }
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Login);
            if (user == null)
            {
                throw new AuthException("Incorrect user login or password");
            }
            if (!user.EmailConfirmed)
            {
                throw new AuthException("Email is not confirmed");
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                throw new AuthException("Incorrect user login or password");
            }

            return await GenerateLoginResponse(user);
        }

        public async Task ResetPassword(ResetPasswordRequest request)
        {
            User user = await _userManager.FindByEmailAsync(request.Login);
            if (user == null)
            {
                throw new AuthException("Incorrect user login or password");
            }
            if (!user.EmailConfirmed)
            {
                throw new AuthException("Email is not confirmed");
            }

            var queryParams = new Dictionary<string, string>
            {
                { "userId", user.Id.ToString() },
                { "code", await _userManager.GeneratePasswordResetTokenAsync(user)}
            };

            string redirectUrl = QueryHelpers.AddQueryString(request.RedirectUrl, queryParams);
            string messageBody = _emailService.GetResetMessage(redirectUrl);
            await _emailService.SendEmailAsync(request.Login, _emailResetSubject, messageBody);
        }


        public async Task SetNewPassword(SetNewPasswordRequest request)
        {
            if (request?.Code == null)
            {
                throw new AuthException("No code provided");
            }

            IdentityResult passwordValidationResult = await ValidatePassword(request.NewPassword);
            if (!passwordValidationResult.Succeeded)
            {
                throw new AuthException(passwordValidationResult.Errors);
            }

            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
            {
                throw new AuthException("Incorrect user ID ");
            }

            bool isEmailTokenValid = await _userManager.VerifyUserTokenAsync(
                user,
                _userManager.Options.Tokens.PasswordResetTokenProvider,
                UserManager<User>.ResetPasswordTokenPurpose,
                request.Code);

            if (!isEmailTokenValid)
            {
                throw new AuthException("Incorrect code", $"Email token is not valid for user {user}");
            }

            await _userManager.RemovePasswordAsync(user);
            IdentityResult passwordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
            if (!passwordResult.Succeeded)
            {
                throw new AuthException(passwordResult.Errors);
            }

        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Secret)),
                ValidateLifetime = false
            };
            ClaimsPrincipal claimPrincipal = new JwtSecurityTokenHandler()
                .ValidateToken(request.AccessToken, tokenValidationParameters, out var validatedToken);
            if (claimPrincipal == null)
            {

                throw new AuthException("Invalid token", "Could not validate access token");
            }

            var nameClaim = claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            if (nameClaim == null || string.IsNullOrEmpty(nameClaim.Value))
            {
                throw new AuthException("Invalid token", "Can't get user name from claims");
            }

            var user = await _userManager.FindByNameAsync(nameClaim.Value);
            if (user == null)
            {
                throw new AuthException("Invalid token", $"User with name {nameClaim.Value} not found");
            }

            var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken);

            if (refreshToken == null || !refreshToken.IsActive)
            {
                throw new AuthException("There is no valid refresh token");
            }

            user.RefreshTokens.Remove(refreshToken);
            await _dbContext.SaveChangesAsync();
            return await GenerateLoginResponse(user);
        }

        private async Task<LoginResponse> GenerateLoginResponse(User user)
        {
            var response = _mapper.Map<LoginResponse>(user);
            var tokenResult = await GenerateJwtToken(user);
            response.AccessToken = tokenResult.Value;
            response.AccessTokenExpiresAt = tokenResult.NowDate.AddMinutes(_settings.AccessTokenValidInMinutes);
            var refreshToken = GenerateRefreshToken();
            _dbContext.RefreshTokens.Add(new RefreshToken { Expires = DateTime.UtcNow.AddDays(_settings.RefreshTokenValidInDays), Token = refreshToken, UserId = user.Id });
            await _dbContext.SaveChangesAsync();
            response.RefreshToken = refreshToken;
            return response;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<(DateTime NowDate, string Value)> GenerateJwtToken(User user)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: _settings.TokenIssuer,
                audience: _settings.TokenAudience,
                notBefore: now,
                claims: (await GetIdentity(user)).Claims,
                expires: now.Add(TimeSpan.FromMinutes(_settings.AccessTokenValidInMinutes)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Secret)), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return (now, encodedJwt);
        }

        private async Task<ClaimsIdentity> GetIdentity(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(", ", await _userManager.GetRolesAsync(user))),
                };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        
        private async Task<IdentityResult> ValidatePassword(string newPassword)
        {
            List<IdentityError> errors = new List<IdentityError>();

            IList<IPasswordValidator<User>> validators = _userManager.PasswordValidators;

            foreach (IPasswordValidator<User> validator in validators)
            {
                IdentityResult result = await validator.ValidateAsync(_userManager, null, newPassword);

                if (!result.Succeeded)
                {
                    errors.AddRange(result.Errors);
                }
            }

            if (errors.Count > 0)
            {
                return IdentityResult.Failed(errors.ToArray());
            }

            return IdentityResult.Success;
        }
    }
}
