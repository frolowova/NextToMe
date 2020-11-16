using AutoMapper;
using NextToMe.Common.DTOs;
using NextToMe.Database;
using NextToMe.Database.Entities;
using NextToMe.Services.ServiceInterfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<MessageCommentResponse> SendComment(AddMessageCommentRequest request)
        {
            request.MessageId ??= _dbContext.MessageComments.First(x => x.Id == request.CommentId).MessageId;

            var newComment = _mapper.Map<MessageComment>(request);
            newComment.CreatedAt = DateTime.UtcNow;
            _dbContext.Add(newComment);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<MessageCommentResponse>(newComment);
        }
    }
}
