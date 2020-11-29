using System;

namespace NextToMe.Common.DTOs
{
    public class MessageResponse : AddMessageRequest
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid From { get; set; }

        public double DistanceToUser { get; set; }

        public DateTime? DeleteAt { get; set; }

        public long Views { get; set; }
    }
}
