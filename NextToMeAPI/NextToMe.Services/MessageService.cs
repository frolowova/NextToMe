using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using NextToMe.Common.DTOs;
using NextToMe.Common.Exceptions;
using NextToMe.Database;
using NextToMe.Database.Entities;
using NextToMe.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using Location = NextToMe.Common.Models.Location;

namespace NextToMe.Services
{
    public class MessageService : IMessageService
    {
        private static readonly TimeSpan _messageDefaultLifetime = TimeSpan.FromDays(1);
        private const int _messageExtraLifeTimeMinutes = 10;

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<MessageService> _logger;

        public MessageService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            UserManager<User> userManager,
            ILogger<MessageService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<MessageResponse>> GetMessages(GetMessageRequest request)
        {
            var userLocation = new Point(request.CurrentLocation.Latitude, request.CurrentLocation.Longitude) { SRID = 4326 };

            List<MessageResponse> messages = 
                ProcessFilters(
                    _dbContext.Messages
                    .Where(x => x.Location.Distance(userLocation) <= request.GettingMessagesRadiusInMeters),
                    request.Filter
                )
                .Select(x => new MessageResponse()
                {
                    DistanceToUser = x.Location.Distance(userLocation),
                    CreatedAt = x.CreatedAt,
                    From = x.User.Id,
                    FromName = x.User.UserName,
                    Text = x.Text,
                    Location = new Location(x.Location.X, x.Location.Y),
                    DeleteAt = x.DeleteAt,
                    LikesCount = x.UserLikedMessages.Count,
                    Id = x.Id,
                    Place = x.Place,
                    Views = x.Views,
                    CommentsCount = x.Comments.Count,
                    Photos = x.MessageImages.Select(image => image.Id)
                })
                .ToList();

            List<Guid> messageIds = messages.Select(x => x.Id).ToList();

            int updatedCount = await _dbContext.Messages
                .Where(x => messageIds.Contains(x.Id))
                .UpdateAsync(x => new Message { Views = x.Views + 1, DeleteAt = x.DeleteAt.Value.AddMinutes(_messageExtraLifeTimeMinutes) });
            _logger.LogInformation($"Get Message: updated {updatedCount} messages");

            return messages;
        }

        public async Task<MessageResponse> SendMessage(AddMessageRequest request)
        {
            User user = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);
            var newMessage = _mapper.Map<Message>(request);
            newMessage.UserId = user.Id;
            newMessage.CreatedAt = DateTime.UtcNow;
            newMessage.DeleteAt = DateTime.UtcNow.Add(_messageDefaultLifetime);

            if (request.Photos != null)
                foreach (var photo in request.Photos)
                    newMessage.MessageImages.Add(new MessageImage() { Image = photo });

            _dbContext.Add(newMessage);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<MessageResponse>(newMessage);
        }

        public async Task LikeMessage(Guid messageId)
        {
            User user = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);
            if (user.UserLikedMessages.Any(x => x.MessageId == messageId))
            {
                throw new BadRequestException("The message has already been liked");
            }

            Message message = _dbContext.Messages.FirstOrDefault(x => x.Id == messageId);
            if (message == null)
            {
                throw new BadRequestException("There is no message with this id");
            }

            UserLikedMessage userLikedMessage = new UserLikedMessage()
            {
                User = user,
                Message = message
            };

            await _dbContext.UserLikedMessages.AddAsync(userLikedMessage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveLikeFromMessage(Guid messageId)
        {
            User user = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

            if (user.UserLikedMessages.All(x => x.MessageId != messageId))
            {
                throw new BadRequestException("The message has not been liked");
            }

            Message message = _dbContext.Messages
                .Include(x => x.UserLikedMessages)
                .FirstOrDefault(x => x.Id == messageId);
            if (message == null)
            {
                throw new BadRequestException("There is no message with this id");
            }

            UserLikedMessage userLikedMessage = _dbContext.UserLikedMessages
                .Where(x => x.MessageId == messageId).First(x => x.UserId == user.Id);

            _dbContext.Remove(userLikedMessage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Dictionary<Guid, string>> GetMessageImages(List<Guid> messageImageIds)
        {
            return await _dbContext.MessageImages
                .Where(x => messageImageIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, x => x.Image);
        }

        private IQueryable<Message> ProcessFilters(IQueryable<Message> collection, MessageFilter filter)
        {
            collection = collection.Where(x => x.CreatedAt > filter.Start && x.CreatedAt <= filter.End);
            if (filter.OrderType == OrderType.Asc)
            {
                return filter.OrderBy switch
                {
                    OrderBy.CreatedDate => collection.OrderBy(x => x.CreatedAt),
                    OrderBy.Views => collection.OrderBy(x => x.Views).ThenBy(x => x.CreatedAt),
                    OrderBy.Comments => collection.OrderBy(x => x.Comments.Count).ThenBy(x => x.CreatedAt),
                    OrderBy.Likes => collection.OrderBy(x => x.UserLikedMessages.Count).ThenBy(x => x.CreatedAt),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            return filter.OrderBy switch
            {
                OrderBy.CreatedDate => collection.OrderByDescending(x => x.CreatedAt),
                OrderBy.Views => collection.OrderByDescending(x => x.Views).ThenByDescending(x => x.CreatedAt),
                OrderBy.Comments => collection.OrderByDescending(x => x.Comments.Count).ThenByDescending(x => x.CreatedAt),
                OrderBy.Likes => collection.OrderByDescending(x => x.UserLikedMessages.Count).ThenByDescending(x => x.CreatedAt),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
