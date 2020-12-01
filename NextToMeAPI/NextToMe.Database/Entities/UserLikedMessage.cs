using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NextToMe.Database.Entities
{
    public class UserLikedMessage
    {
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public Guid MessageId { get; set; }

        [ForeignKey("UserId")]
        public virtual Message Message { get; set; }
    }
}
