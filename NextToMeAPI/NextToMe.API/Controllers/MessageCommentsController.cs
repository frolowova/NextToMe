﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextToMe.Common.DTOs;
using NextToMe.Common.Models;
using NextToMe.Services.ServiceInterfaces;
using System.Threading.Tasks;

namespace NextToMe.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.User)]
    [Route("api/message/comments")]
    public class MessageCommentsController : ControllerBase
    {
        private readonly IMessageCommentService _messageCommentService;

        public MessageCommentsController(IMessageCommentService messageCommentService)
        {
            _messageCommentService = messageCommentService;
        }

        [HttpPost]
        [Route("send")]
        public async Task<MessageCommentResponse> SendComment(AddMessageCommentRequest request)
        {
            return await _messageCommentService.SendComment(request);
        }

        [HttpGet]
        [Route("get")]
        public async Task<List<MessageCommentResponse>> GetComments(Guid messageId, int skip = 0, int take = int.MaxValue)
        {
            return await _messageCommentService.GetComments(skip, take, messageId);
        }

        [HttpGet]
        [Route("get/from/user")]
        public async Task<List<MessageCommentResponse>> GetAllCommentsOfUserMessages()
        {
            return await _messageCommentService.GetAllCommentsOfUserMessages();
        }
    }
}
