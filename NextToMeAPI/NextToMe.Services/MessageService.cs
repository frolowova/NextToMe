using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using System.Xml.XPath;
using NextToMe.Common;
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
            var userLocation = new Point(currentLocation.Latitude, currentLocation.Longitude);

            var distance = DistanceConverter.DistanceInMeters(_dbContext.Messages.ToList()[0].Location, userLocation);

            return Task.FromResult(_dbContext.Messages
                .Where(x => DistanceConverter.DistanceInMeters(x.Location, userLocation) <= gettingMessagesRadiusInMeters)
                .OrderBy(x => x.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ProjectTo<MessageResponse>(_mapper.ConfigurationProvider)
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
