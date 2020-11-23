using AutoMapper;
using NextToMe.Common.DTOs;
using NextToMe.Database;
using NextToMe.Database.Entities;
using NextToMe.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NextToMe.Common.Exceptions;

namespace NextToMe.Services
{
    public class MessageCommentService : IMessageCommentService
    {
        private readonly TimeSpan _messageExtraLifeTime = TimeSpan.FromMinutes(10);

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public MessageCommentService(
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

        public Task<List<MessageCommentResponse>> GetComments(int skip, int take, string messageId)
        {
            return Task.FromResult(_dbContext.MessageComments
                .Where(x => x.MessageId == messageId)
                .OrderBy(x => x.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ProjectTo<MessageCommentResponse>(_mapper.ConfigurationProvider)
                .ToList());
        }

        public async Task<MessageCommentResponse> SendComment(AddMessageCommentRequest request)
        {
            User user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
            if (request.MessageId != null)
            {
                MessageComment comment = _dbContext.MessageComments.FirstOrDefault(x => x.MessageId == request.MessageId);
                if (comment != null)
                {
                    request.MessageId = comment.MessageId;
                }
                else
                {
                    if (_dbContext.Messages.All(x => x.Id != request.MessageId))
                    {
                        throw new BadRequestException($"MessageId = {request.MessageId} does not exists");
                    }
                }
            }
            else
            {
                throw new BadRequestException($"MessageId = {request.MessageId} does not exists");
            }

            var newComment = _mapper.Map<MessageComment>(request);
            newComment.UserId = user.Id;
            newComment.CreatedAt = DateTime.UtcNow;
            _dbContext.Add(newComment);
            Message message = _dbContext.Messages.FirstOrDefault(x => x.Id == newComment.MessageId);
            if (message != null)
            {
                message.DeleteAt = message.DeleteAt?.Add(_messageExtraLifeTime);
                _dbContext.Entry(message).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<MessageCommentResponse>(newComment);
        }
    }
}
