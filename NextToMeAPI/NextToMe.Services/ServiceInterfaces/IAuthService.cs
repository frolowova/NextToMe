using NextToMe.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextToMe.Services.ServiceInterfaces
{
    public interface IAuthService
    {
        public Task<LoginResponse> Register(LoginRequest request);
        public Task<LoginResponse> Login(LoginRequest request);
        public Task<LoginResponse> RefreshToken(RefreshTokenRequest request);
    }
}
