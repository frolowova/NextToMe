using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NextToMe.Common.DTOs
{
    public enum OrderBy
    {
        CreatedDate,
        Views,
        Comments,
        Likes
    }

    public enum OrderType
    {
        Asc,
        Desc
    }

    public class MessageFilter
    {
        public DateTime Start { get; set; } = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1));

        public DateTime End { get; set; } = DateTime.UtcNow;

        public OrderBy OrderBy { get; set; }

        public OrderType OrderType { get; set; }
    }
}
