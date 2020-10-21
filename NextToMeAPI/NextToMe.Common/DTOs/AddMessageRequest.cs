using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NextToMe.Common.DTOs
{
    public class AddMessageRequest
    {
        [Required]
        public string Text { get; set; }
    }
}
