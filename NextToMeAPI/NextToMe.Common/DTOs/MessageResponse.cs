using System;
using System.Collections.Generic;
using NextToMe.Common.Models;

namespace NextToMe.Common.DTOs
{
    public class MessageResponse
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Location Location { get; set; }

        public string Place { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid From { get; set; }

        public int LikesCount { get; set; }

        public double DistanceToUser { get; set; }

        public DateTime? DeleteAt { get; set; }

        public long Views { get; set; }

        public long CommentsCount { get; set; }

        public IEnumerable<Guid> Photos { get; set; }
    }
}
