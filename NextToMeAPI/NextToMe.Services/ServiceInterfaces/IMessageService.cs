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
        public Task<List<MessageResponse>> GetMessages(GetMessageRequest request);
        public Task LikeMessage(Guid messageId);
        public Task RemoveLikeFromMessage(Guid messageId);
        public Task<Dictionary<Guid, string>> GetMessageImages(List<Guid> messageImageIds);
    }
}
