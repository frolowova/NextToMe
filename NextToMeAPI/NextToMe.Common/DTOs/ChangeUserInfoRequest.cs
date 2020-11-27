using System;
using System.ComponentModel.DataAnnotations;

namespace NextToMe.Common.DTOs
{
    public class ChangeUserInfoRequest
    {
        [Required]
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string ImageBase64 { get; set; }
    }

}
