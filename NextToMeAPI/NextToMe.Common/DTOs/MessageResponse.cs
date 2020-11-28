using System;

namespace NextToMe.Common.DTOs
{
    public class MessageResponse : AddMessageRequest
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string From { get; set; }

        public int LikesCount { get; set; }

        public double DistanceToUser { get; set; }

        public DateTime? DeleteAt { get; set; }

        public long Views { get; set; }
    }
}
