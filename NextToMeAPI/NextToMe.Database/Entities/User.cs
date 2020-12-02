using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace NextToMe.Database.Entities
{
    public class User : IdentityUser<Guid>
    {
        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();

        public virtual ICollection<MessageComment> MessageComments { get; set; } = new HashSet<MessageComment>();

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();

        public virtual UserImage UserImage { get; set; }

        public virtual ICollection<UserLikedMessage> UserLikedMessages { get; set; } = new HashSet<UserLikedMessage>();
    }
}
