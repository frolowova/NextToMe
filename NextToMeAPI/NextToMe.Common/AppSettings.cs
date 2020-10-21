using System;
using System.Collections.Generic;
using System.Text;

namespace NextToMe.Common
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int AccessTokenValidInMinutes { get; set; }
        public int RefreshTokenValidInDays { get; set; }
        public string TokenIssuer { get; set; }
        public string TokenAudience { get; set; }
    }
}
