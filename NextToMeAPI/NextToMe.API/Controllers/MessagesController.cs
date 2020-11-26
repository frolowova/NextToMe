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
        /// Get Messages ordered by created date
        /// </summary>
        [HttpPost]
        [Route("get")]
        public async Task<List<MessageResponse>> GetMessages(GetMessageRequest request)
        {
            return await _messageService.GetMessages(request.Skip, request.Take, request.CurrentLocation, request.GettingMessagesRadiusInMeters);
        }
    }
}
