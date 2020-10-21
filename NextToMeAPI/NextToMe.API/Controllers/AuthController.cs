using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NextToMe.Common.DTOs;
using NextToMe.Common.Models;
using NextToMe.Services.ServiceInterfaces;

namespace NextToMe.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await _authService.Login(request);
        }

        [HttpPost]
        [Route("register")]
        public async Task<LoginResponse> Register(LoginRequest request)
        {
            return await _authService.Register(request);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
        {
            return await _authService.RefreshToken(request);
        }
    }
}
