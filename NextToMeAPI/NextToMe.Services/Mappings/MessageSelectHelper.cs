using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTopologySuite.Geometries;
using NextToMe.Common.DTOs;
using NextToMe.Database.Entities;

namespace NextToMe.Services.Mappings
{
    public static class MessageSelectHelper
    {
        public static IQueryable<MessageResponse> SelectWithLocation(this IQueryable<Message> messages, Point location)
        {
            return messages.Select(x => new MessageResponse
            {
                DistanceToUser = x.Location.Distance(location),
                CreatedAt = x.CreatedAt,
                From = x.User.Id,
                FromName = x.User.UserName,
                Text = x.Text,
                Location = new Common.Models.Location(x.Location.X, x.Location.Y),
                DeleteAt = x.DeleteAt,
                LikesCount = x.UserLikedMessages.Count,
                Id = x.Id,
                Place = x.Place,
                Views = x.Views,
                CommentsCount = x.Comments.Count,
                Photos = x.MessageImages.Select(image => image.Id)
            });
        }
    }
}
