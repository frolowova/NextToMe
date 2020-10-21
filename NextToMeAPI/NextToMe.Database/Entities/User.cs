using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextToMe.Database.Entities
{
    public class User : IdentityUser<Guid>
    {
        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    }
}
