using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NextToMe.Common.DTOs
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public string RefreshToken { get; set; }
    }
}
