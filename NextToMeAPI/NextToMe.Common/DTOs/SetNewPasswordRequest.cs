using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NextToMe.Common.DTOs
{
    public class SetNewPasswordRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
