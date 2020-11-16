using System;

namespace NextToMe.Common.DTOs
{
    public class MessageResponse : AddMessageRequest
    {
        public DateTime CreatedAt { get; set; }

        public string From { get; set; }

        public double DistanceToUser { get; set; }
    }
}
