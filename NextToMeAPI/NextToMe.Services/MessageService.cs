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
using System.Threading.Tasks;
using Location = NextToMe.Common.Models.Location;

namespace NextToMe.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public MessageService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public Task<List<MessageResponse>> GetMessages(int skip, int take, Location currentLocation, int gettingMessagesRadiusInMeters = 500)
        {
            var userLocation = new Point(currentLocation.Latitude, currentLocation.Longitude) { SRID = 4326 };

            return Task.FromResult(_dbContext.Messages
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
                    DeleteAt = x.DeleteAt
                })
                .ToList());
        }

        public async Task<MessageResponse> SendMessage(AddMessageRequest request)
        {
            User user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
            var newMessage = _mapper.Map<Message>(request);
            newMessage.UserId = user.Id;
            newMessage.CreatedAt = DateTime.UtcNow;
            _dbContext.Add(newMessage);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<MessageResponse>(newMessage);
        }
    }
}
