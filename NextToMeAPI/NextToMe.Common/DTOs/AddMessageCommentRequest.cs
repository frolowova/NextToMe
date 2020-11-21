using System;
using System.ComponentModel.DataAnnotations;

namespace NextToMe.Common.DTOs
{
    public class AddMessageCommentRequest
    {
        [Required]
        public string Text { get; set; }

        public string MessageId { get; set; }

        public string CommentId { get; set; }
    }
}
