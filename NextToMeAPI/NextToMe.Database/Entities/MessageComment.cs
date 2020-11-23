using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextToMe.Database.Entities
{
    public class MessageComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserName => Message.User.UserName;

        [Required]
        public string MessageId { get; set; }
        
        [ForeignKey("MessageId")]
        public virtual Message Message { get; set; }
    }
}
