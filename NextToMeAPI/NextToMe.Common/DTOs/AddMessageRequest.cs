using System;
using System.ComponentModel.DataAnnotations;

namespace NextToMe.Common.DTOs
{
    public class AddMessageRequest
    {
        [Required]
        public string Text { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
