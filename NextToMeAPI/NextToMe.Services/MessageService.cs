using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using NextToMe.Common.DTOs;
using NextToMe.Database;
using NextToMe.Database.Entities;
using NextToMe.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NextToMe.Common.Exceptions;
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

        public async Task<List<MessageResponse>> GetMessages(int skip, int take, Location currentLocation, int gettingMessagesRadiusInMeters = 500)
        {

            var userLocation = new Point(currentLocation.Latitude, currentLocation.Longitude) { SRID = 4326 };

            List<MessageResponse> messages = _dbContext.Messages
                .Where(x => x.Location.Distance(userLocation) <= gettingMessagesRadiusInMeters)
                .OrderBy(x => x.CreatedAt)
                .Skip(skip)
                .Take(take)
                .Select(x => new MessageResponse()
                {
                    DistanceToUser = x.Location.Distance(userLocation),
                    CreatedAt = x.CreatedAt,
                    From = x.User.UserName,
                    Text = x.Text,
                    Location = new Location(x.Location.X, x.Location.Y),
                    DeleteAt = x.DeleteAt,
                    Id = x.Id,
                    Place = x.Place,
                    Views = x.Views
                })
                .ToList();

            List<Guid> messageIds = messages.Select(x => x.Id).ToList();
            if (!_dbContext.IsInMemory())
            {
                int updatedCount = await _dbContext.Messages
                    .Where(x => messageIds.Contains(x.Id))
                    .UpdateAsync(x => new Message { Views = x.Views + 1, DeleteAt = x.DeleteAt.Value.AddMinutes(_messageExtraLifeTimeMinutes) });
                _logger.LogInformation($"Get Message: updated {updatedCount} messages");
            }

            return messages;
        }

        public async Task<MessageResponse> SendMessage(AddMessageRequest request)
        {
            User user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
            var newMessage = _mapper.Map<Message>(request);
            newMessage.UserId = user.Id;
            newMessage.CreatedAt = DateTime.UtcNow;
            newMessage.DeleteAt = DateTime.UtcNow.Add(_messageDefaultLifetime);
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
        }

        public async Task RemoveLikeFromMessage(Guid messageId)
        {
            User user = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

        }

        public async Task GetMessageLikesCount(Guid messageId)
        {
            User user = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

        }
    }
}
