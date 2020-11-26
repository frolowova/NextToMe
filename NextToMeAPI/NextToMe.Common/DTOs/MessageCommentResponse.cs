using System;
using System.Collections.Generic;
using System.Text;

namespace NextToMe.Common.DTOs
{
    public class MessageCommentResponse : AddMessageCommentRequest
    {
        public string From { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
