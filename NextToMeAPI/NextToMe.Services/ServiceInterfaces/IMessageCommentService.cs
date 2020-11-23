using System;
using NextToMe.Common.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextToMe.Services.ServiceInterfaces
{
    public interface IMessageCommentService
    {
        public Task<List<MessageCommentResponse>> GetComments(int skip, int take, string messageId);
        public Task<MessageCommentResponse> SendComment(AddMessageCommentRequest request);
    }
}
