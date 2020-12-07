using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextToMe.Common.DTOs;
using NextToMe.Common.Models;
using NextToMe.Services.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextToMe.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.User)]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [Route("send")]
        public async Task<MessageResponse> SendMessage(AddMessageRequest request)
        {
            return await _messageService.SendMessage(request);
        }

        /// <summary>
        /// Get Messages ordered with a filter
        /// </summary>
        [HttpPost]
        [Route("get")]
        public async Task<List<MessageResponse>> GetMessages(GetMessageRequest request)
        {
            return await _messageService.GetMessages(request);
        }

        [HttpPost]
        [Route("like")]
        public async Task LikeMessage(Guid messageId)
        {
            await _messageService.LikeMessage(messageId);
        }

        [HttpPost]
        [Route("like/remove")]
        public async Task RemoveLike(Guid messageId)
        {
            await _messageService.RemoveLikeFromMessage(messageId);
        }

        [HttpPost]
        [Route("images/get")]
        public async Task<Dictionary<Guid, string>> GetMessageImages(List<Guid> messageImageIds)
        {
            return await _messageService.GetMessageImages(messageImageIds);
        }
    }
}
