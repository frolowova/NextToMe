using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextToMe.Database.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        public Point Location { get; set; }

        public string Place { get; set; }

        public long Views { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<MessageComment> Comments { get; set; } = new HashSet<MessageComment>();

        public virtual ICollection<UserLikedMessage> UserLikedMessages { get; set; } = new HashSet<UserLikedMessage>();

        public virtual ICollection<MessageImage> MessageImages { get; set; } = new HashSet<MessageImage>();
    }
}
