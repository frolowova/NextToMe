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

        [Required] 
        public int Skip { get; set; } = 0;

        [Required] 
        public int Take { get; set; } = int.MaxValue;

        [Required]
        public int GettingMessagesRadiusInMeters { get; set; } = 500;
    }
}
