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
using NextToMe.Common.Exceptions;

namespace NextToMe.Services
{
    public class MessageCommentService : IMessageCommentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MessageCommentService(
            ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<List<MessageCommentResponse>> GetComments(int skip, int take, string messageId)
        {
            return Task.FromResult(_dbContext.MessageComments
                .Where(x => x.MessageId == messageId)
                .Skip(skip)
                .Take(take)
                .ProjectTo<MessageCommentResponse>(_mapper.ConfigurationProvider)
                .ToList());
        }

        public async Task<MessageCommentResponse> SendComment(AddMessageCommentRequest request)
        {
            if (request.MessageId != null)
            {
                if (_dbContext.Messages.Any(x => x.Id == request.MessageId == false))
                {
                    throw new BadRequestException($"MessageId = {request.MessageId} does not exists");
                }
            }
            else
            {
                if (_dbContext.MessageComments.Any(x => x.Id == request.CommentId))
                {
                    request.MessageId = _dbContext.MessageComments.First(x => x.Id == request.CommentId).MessageId;
                }
                else
                {
                    throw new BadRequestException($"MessageId = {request.MessageId} does not exists");
                }
            }

            var newComment = _mapper.Map<MessageComment>(request);
            newComment.CreatedAt = DateTime.UtcNow;
            _dbContext.Add(newComment);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<MessageCommentResponse>(newComment);
        }
    }
}
