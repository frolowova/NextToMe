using System;
using System.Collections.Generic;
using System.Text;

namespace NextToMe.Common.DTOs
{
    public class MessageCommentResponse : AddMessageCommentRequest
    {
        public Guid From { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
