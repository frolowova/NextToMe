using NextToMe.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextToMe.Services.ServiceInterfaces
{
    public interface IAuthService
    {
        public Task Register(RegisterRequest request);
        public Task ConfirmEmail(EmailConfirmRequest request);
        public Task<LoginResponse> Login(LoginRequest request);
        public Task<LoginResponse> RefreshToken(RefreshTokenRequest request);
    }
}
