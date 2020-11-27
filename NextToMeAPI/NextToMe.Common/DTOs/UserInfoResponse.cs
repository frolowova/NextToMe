using System;
using System.Collections.Generic;
using System.Text;

namespace NextToMe.Common.DTOs
{
    public class UserInfoResponse
    {
        public Guid UserId { get; set; }

        public string ImageBase64 { get; set; }

        public string UserName { get; set; }
    }
}
