using NextToMe.Common.DTOs;
using NextToMe.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextToMe.Services.ServiceInterfaces
{
    public interface IMessageService
    {
        public Task<MessageResponse> SendMessage(AddMessageRequest request);
        public Task<List<MessageResponse>> GetMessages(int skip, int take, Location currentLocation, int gettingMessagesRadiusInMeters = 500);
        public Task LikeMessage(Guid messageId);
        public Task RemoveLikeFromMessage(Guid messageId);
        public Task<string> GetMessageImage(Guid messageImageId);
        public Task<List<Guid>> GetIdsOfUserMessages();
        public Task<List<MessageResponse>> GetMessagesFromId(List<Guid> ids);
    }
}
