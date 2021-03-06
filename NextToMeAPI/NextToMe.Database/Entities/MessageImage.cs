﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextToMe.Database.Entities
{
    public class MessageImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Image { get; set; }

        public Guid MessageId { get; set; }

        [ForeignKey("MessageId")]
        public virtual Message Message { get; set; }
    }
}
