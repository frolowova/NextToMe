﻿using AutoMapper;
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
using NextToMe.Services.Mappings;
using Z.EntityFramework.Plus;
using Location = NextToMe.Common.Models.Location;

namespace NextToMe.Services
{
    public class MessageService : IMessageService
    {
        private static readonly TimeSpan _messageDefaultLifetime = TimeSpan.FromDays(1);
        private const int _messageExtraLifeTimeMinutes = 10;
        private const int _minimumViewsTopCount = 3;

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

        public async Task<List<Guid>> GetIdsOfUserMessages()
        {
            User user = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);
            return await Task.FromResult(_dbContext.Messages
                .Where(x => x.User.Id == user.Id)
                .Select(x => x.Id)
                .ToList());
        }

        public Task<List<MessageResponse>> GetMessagesFromId(List<Guid> ids)
        {
            return Task.FromResult(_dbContext.Messages
                .Where(x => ids.Contains(x.Id))
                .OrderBy(x => x.CreatedAt)
                .Select(x => new MessageResponse()
                {
                    CreatedAt = x.CreatedAt,
                    From = x.User.Id,
                    Text = x.Text,
                    Location = new Location(x.Location.X, x.Location.Y),
                    DeleteAt = x.DeleteAt,
                    LikesCount = x.UserLikedMessages.Count,
                    Id = x.Id,
                    FromName = x.User.UserName,
                    Place = x.Place,
                    Views = x.Views,
                    CommentsCount = x.Comments.Count,
                    Photos = x.MessageImages.Select(image => image.Id)
                })
                .ToList());
        }
        public async Task<List<MessageResponse>> GetMessages(int skip, int take, Location currentLocation, int gettingMessagesRadiusInMeters = 500)
        {
            var userLocation = new Point(currentLocation.Latitude, currentLocation.Longitude) { SRID = 4326 };

            List<MessageResponse> messages = await _dbContext.Messages
                .Where(x => x.Location.Distance(userLocation) <= gettingMessagesRadiusInMeters)
                .OrderBy(x => x.CreatedAt)
                .Skip(skip)
                .Take(take)
                .SelectWithLocation(userLocation)
                .ToListAsync();
            
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
            _logger.LogError(_contextAccessor.HttpContext.User.Identity.Name);
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

        public Task<string> GetMessageImage(Guid messageImageId)
        {
            return Task.FromResult(_dbContext.MessageImages
                .Where(x => x.Id == messageImageId)
                .Select(x => x.Image)
                .First());
        }

        public async Task AddViewToMessage(List<Guid> messageIds)
        {
            int updatedCount = await _dbContext.Messages
                .Where(x => messageIds.Contains(x.Id))
                .UpdateAsync(x => new Message { Views = x.Views + 1, DeleteAt = x.DeleteAt.Value.AddMinutes(_messageExtraLifeTimeMinutes) });
            _logger.LogInformation($"Get Message: updated {updatedCount} messages");
        }

        public async Task<List<MessageResponse>> GetTopViewed(SkipTakeMessagesRequest request)
        {
            List<MessageResponse> messages = await _dbContext.Messages
                .Where(x => x.Views >= _minimumViewsTopCount)
                .OrderByDescending(x => x.Views)
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(x => new MessageResponse()
                {
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
                .ToListAsync();

            return messages;
        }
    }
}
