using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NextToMe.Common;
using NextToMe.Common.DTOs;
using NextToMe.Common.Exceptions;
using NextToMe.Common.Models;
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

namespace NextToMe.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AppSettings _settings;
        private readonly UserManager<User> _userManager;

        public AuthService(
            ApplicationDbContext dbContext,
            UserManager<User> userManager,
            ILogger<AuthService> logger,
            IMapper mapper,
            IOptions<AppSettings> settings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _settings = settings.Value;
            _userManager = userManager;
        }

        public async Task<LoginResponse> Register(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Login);
            if (user != null)
            {
                throw new BadRequestException("Login is already taken");
            }

            user = new User { Email = request.Login, UserName = request.Login };
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
            }
            else
            {
                await _userManager.DeleteAsync(user);
                throw new AuthException(result.Errors);
            }

            return await GenerateLoginResponse(user);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Login);
            if (user == null)
            {
                throw new AuthException("Incorrect user login or password");
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                throw new AuthException("Incorrect user login or password");
            }

            return await GenerateLoginResponse(user);
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

    }
}
