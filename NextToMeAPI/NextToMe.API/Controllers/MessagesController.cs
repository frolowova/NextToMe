using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NextToMe.Common.DTOs;
using NextToMe.Common.Models;
using NextToMe.Services.ServiceInterfaces;

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
        public async Task<AddMessageResponse> SendMessage(AddMessageRequest request)
        {
            return await _messageService.SendMessage(request);
        }

        /// <summary>
        /// Get Messages ordered by created date
        /// </summary>
        [HttpGet]
        [Route("get")]
        public async Task<List<AddMessageResponse>> GetMessages(int skip = 0, int take = int.MaxValue)
        {
            return await _messageService.GetMessages(skip, take);
        }
    }
}
