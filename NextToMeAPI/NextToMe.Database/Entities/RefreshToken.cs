using System;
using System.ComponentModel.DataAnnotations;

namespace NextToMe.Database.Entities
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public Guid UserId { get; set; }

        public bool IsActive => DateTime.UtcNow <= Expires;

        public virtual User User { get; set; }
    }
}
