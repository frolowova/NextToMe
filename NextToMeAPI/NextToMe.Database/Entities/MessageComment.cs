using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextToMe.Database.Entities
{
    public class MessageComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }
        
        [Required]
        public Guid MessageId { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("MessageId")]
        public virtual Message Message { get; set; }

        public virtual User User { get; set; }
    }
}
