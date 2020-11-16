using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NextToMe.Common.DTOs
{
    public class AddMessageCommentRequest
    {
        [Required]
        public string Text { get; set; }

        public string MessageId { get; set; }

        public Guid CommentId { get; set; }

    }
}
