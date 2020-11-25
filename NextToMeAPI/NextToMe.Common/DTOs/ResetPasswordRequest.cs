using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NextToMe.Common.DTOs
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string RedirectUrl { get; set; }
    }
}
