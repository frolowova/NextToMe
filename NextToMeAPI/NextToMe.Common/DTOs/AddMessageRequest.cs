using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Location = NextToMe.Common.Models.Location;

namespace NextToMe.Common.DTOs
{
    public class AddMessageRequest
    {
        [Required]
        public string Text { get; set; }

        public Location Location { get; set; }

        public string Place { get; set; }

        public ICollection<string> Photos { get; set; }
    }
}
