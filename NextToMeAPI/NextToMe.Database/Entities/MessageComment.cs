using System;
using System.ComponentModel.DataAnnotations;

namespace NextToMe.Database.Entities
{
    public class MessageComment
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserName => Message.User.UserName;

        [Required]
        public  string MessageId { get; set; }

        public virtual Message Message { get; set; }
    }
}
