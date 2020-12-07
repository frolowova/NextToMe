using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NextToMe.Common.Models;

namespace NextToMe.Common.DTOs
{
    public class GetMessageRequest
    {
        [Required]
        public Location CurrentLocation { get; set; }

        public MessageFilter Filter { get; set; } = new MessageFilter{Start = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)), End = DateTime.UtcNow };

        [Required]
        public int GettingMessagesRadiusInMeters { get; set; } = 500;
    }
}
